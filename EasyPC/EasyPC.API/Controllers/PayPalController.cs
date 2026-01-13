using EasyPC.Model.Requests.OrderRequests;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace EasyPC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayPalController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PayPalController> _logger;

        // Store pending orders temporarily (in production use Redis/database)
        private static readonly Dictionary<string, OrderInsertRequest> _pendingOrders = new();

        public PayPalController(
            IOrderService orderService,
            IConfiguration configuration,
            ILogger<PayPalController> logger)
        {
            _orderService = orderService;
            _configuration = configuration;
            _logger = logger;
        }

        private PayPalHttpClient GetPayPalClient()
        {
            var clientId = _configuration["PayPal:ClientId"];
            var secret = _configuration["PayPal:Secret"];
            var mode = _configuration["PayPal:Mode"];

            var environment = mode == "sandbox"
                ? new SandboxEnvironment(clientId, secret)
                : (PayPalEnvironment)new LiveEnvironment(clientId, secret);

            return new PayPalHttpClient(environment);
        }

        [AllowAnonymous]
        [HttpPost("create-order")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderInsertRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.OrderDetails == null || orderRequest.OrderDetails.Count == 0)
            {
                return BadRequest("Invalid order request");
            }

            try
            {
                var client = GetPayPalClient();
                var totalAmount = orderRequest.OrderDetails.Sum(od => od.Quantity * od.UnitPrice);

                // Create PayPal order request
                var order = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>
                    {
                        new PurchaseUnitRequest
                        {
                            AmountWithBreakdown = new AmountWithBreakdown
                            {
                                CurrencyCode = "USD",
                                Value = totalAmount.ToString("F2")
                            },
                            Description = $"EasyPC Order - {orderRequest.OrderDetails.Count} item(s)"
                        }
                    },
                    ApplicationContext = new ApplicationContext
                    {
                        ReturnUrl = "https://easypc.com/paypal/success",
                        CancelUrl = "https://easypc.com/paypal/cancel",
                        BrandName = "EasyPC",
                        UserAction = "PAY_NOW"
                    }
                };

                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(order);

                var response = await client.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

                // Store the order request for later capture
                _pendingOrders[result.Id] = orderRequest;

                // Find the approval URL
                var approvalUrl = result.Links.FirstOrDefault(l => l.Rel == "approve")?.Href;

                _logger.LogInformation("PayPal order created: {OrderId}, Approval URL: {ApprovalUrl}", result.Id, approvalUrl);

                return Ok(new
                {
                    orderId = result.Id,
                    status = result.Status,
                    approvalUrl = approvalUrl,
                    links = result.Links.Select(l => new { rel = l.Rel, href = l.Href })
                });
            }
            catch (HttpException ex)
            {
                _logger.LogError(ex, "PayPal HTTP error creating order: {StatusCode}", ex.StatusCode);
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PayPal order");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("capture-order/{paypalOrderId}")]
        public async Task<ActionResult> CaptureOrder(string paypalOrderId)
        {
            if (string.IsNullOrEmpty(paypalOrderId))
            {
                return BadRequest("PayPal Order ID is required");
            }

            try
            {
                var client = GetPayPalClient();
                
                // First check the order status
                var getRequest = new OrdersGetRequest(paypalOrderId);
                var getResponse = await client.Execute(getRequest);
                var orderDetails = getResponse.Result<PayPalCheckoutSdk.Orders.Order>();
                
                _logger.LogInformation("PayPal order status before capture: {OrderId}, Status: {Status}", 
                    paypalOrderId, orderDetails.Status);

                // Order must be APPROVED before we can capture
                if (orderDetails.Status != "APPROVED")
                {
                    return BadRequest(new { 
                        error = $"Order is not approved yet. Current status: {orderDetails.Status}",
                        status = orderDetails.Status
                    });
                }

                // Capture the PayPal order
                var request = new OrdersCaptureRequest(paypalOrderId);
                request.Prefer("return=representation");
                request.RequestBody(new OrderActionRequest());

                var response = await client.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

                _logger.LogInformation("PayPal order captured: {OrderId}, Status: {Status}", result.Id, result.Status);

                if (result.Status == "COMPLETED")
                {
                    // Get the stored order request
                    if (_pendingOrders.TryGetValue(paypalOrderId, out var orderRequest))
                    {
                        orderRequest.PaymentMethod = "PayPal";
                        orderRequest.PaymentStatus = "Completed";
                        orderRequest.PayPalOrderId = paypalOrderId;
                        
                        var createdOrder = _orderService.Insert(orderRequest);
                        _pendingOrders.Remove(paypalOrderId);

                        return Ok(new
                        {
                            success = true,
                            message = "Payment captured successfully",
                            orderId = createdOrder?.Id,
                            paypalOrderId = paypalOrderId,
                            paypalStatus = result.Status
                        });
                    }
                    else
                    {
                        return BadRequest(new { error = "Order request not found. Please try again." });
                    }
                }

                return BadRequest(new { error = $"Payment not completed. Status: {result.Status}" });
            }
            catch (HttpException ex)
            {
                _logger.LogError(ex, "PayPal HTTP error capturing order: {StatusCode}", ex.StatusCode);
                return StatusCode((int)ex.StatusCode, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error capturing PayPal order");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("order-status/{paypalOrderId}")]
        public async Task<ActionResult> GetOrderStatus(string paypalOrderId)
        {
            try
            {
                var client = GetPayPalClient();
                var request = new OrdersGetRequest(paypalOrderId);
                
                var response = await client.Execute(request);
                var result = response.Result<PayPalCheckoutSdk.Orders.Order>();

                return Ok(new
                {
                    orderId = result.Id,
                    status = result.Status,
                    createTime = result.CreateTime,
                    updateTime = result.UpdateTime
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting PayPal order status");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("return")]
        public IActionResult Return([FromQuery] string token)
        {
            return Ok(new { message = "Payment approved", paypalOrderId = token });
        }

        [AllowAnonymous]
        [HttpGet("cancel")]
        public IActionResult Cancel([FromQuery] string token)
        {
            // Remove pending order if cancelled
            if (!string.IsNullOrEmpty(token) && _pendingOrders.ContainsKey(token))
            {
                _pendingOrders.Remove(token);
            }
            return Ok(new { message = "Payment cancelled", paypalOrderId = token });
        }
    }

    public class PayPalCaptureRequest
    {
        public string? PayPalOrderId { get; set; }
        public OrderInsertRequest? OrderRequest { get; set; }
    }
}
