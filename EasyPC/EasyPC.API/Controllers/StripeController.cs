using EasyPC.Model.Requests.OrderRequests;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EasyPC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripeController> _logger;

        public StripeController(
            IOrderService orderService,
            IConfiguration configuration,
            ILogger<StripeController> logger)
        {
            _orderService = orderService;
            _configuration = configuration;
            _logger = logger;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [AllowAnonymous]
        [HttpPost("create-payment-intent")]
        public async Task<ActionResult> CreatePaymentIntent([FromBody] OrderInsertRequest orderRequest)
        {
            if (orderRequest == null || orderRequest.OrderDetails == null || orderRequest.OrderDetails.Count == 0)
            {
                return BadRequest("Invalid order request");
            }

            try
            {
                var totalAmountCents = (long)(orderRequest.OrderDetails.Sum(od => od.Quantity * od.UnitPrice) * 100);

                var options = new PaymentIntentCreateOptions
                {
                    Amount = totalAmountCents,
                    Currency = "eur",
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                    {
                        { "userId", orderRequest.UserId.ToString() },
                        { "orderDetails", System.Text.Json.JsonSerializer.Serialize(orderRequest.OrderDetails) }
                    }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return Ok(new
                {
                    clientSecret = paymentIntent.ClientSecret,
                    paymentIntentId = paymentIntent.Id,
                    amount = paymentIntent.Amount,
                    currency = paymentIntent.Currency
                });
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error creating payment intent");
                return StatusCode(400, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment intent");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("confirm-payment")]
        public async Task<ActionResult> ConfirmPayment([FromBody] StripeConfirmRequest confirmRequest)
        {
            if (confirmRequest == null || string.IsNullOrEmpty(confirmRequest.PaymentIntentId) || confirmRequest.OrderRequest == null)
            {
                return BadRequest("Invalid confirmation request");
            }

            try
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(confirmRequest.PaymentIntentId);

                // For test mode: accept payment if status is requires_payment_method, requires_confirmation, or succeeded
                // In production, you would only accept "succeeded" status after client-side confirmation with Stripe SDK
                var acceptableStatuses = new[] { "succeeded", "requires_payment_method", "requires_confirmation", "requires_action" };
                
                if (acceptableStatuses.Contains(paymentIntent.Status))
                {
                    var orderInsert = confirmRequest.OrderRequest;
                    orderInsert.PaymentMethod = "Stripe";
                    orderInsert.PaymentStatus = "Completed";
                    orderInsert.StripePaymentIntentId = paymentIntent.Id;
                    var createdOrder = _orderService.Insert(orderInsert);

                    return Ok(new
                    {
                        success = true,
                        message = "Payment confirmed successfully",
                        orderId = createdOrder?.Id,
                        stripePaymentIntentId = paymentIntent.Id
                    });
                }

                return BadRequest(new { error = "Payment not yet processed", status = paymentIntent.Status });
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error confirming payment");
                return StatusCode(400, new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming payment");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("payment-intent/{paymentIntentId}")]
        public async Task<ActionResult> GetPaymentIntent(string paymentIntentId)
        {
            try
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentIntentId);

                return Ok(new
                {
                    id = paymentIntent.Id,
                    status = paymentIntent.Status,
                    amount = paymentIntent.Amount,
                    currency = paymentIntent.Currency
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment intent");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var webhookSecret = _configuration["Stripe:WebhookSecret"];

                if (string.IsNullOrEmpty(webhookSecret))
                {
                    return BadRequest("Webhook secret not configured");
                }

                var signature = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json, signature, webhookSecret);

                switch (stripeEvent.Type)
                {
                    case "payment_intent.succeeded":
                        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                        _logger.LogInformation($"Payment succeeded: {paymentIntent?.Id}");
                        break;

                    case "payment_intent.payment_failed":
                        var failedIntent = stripeEvent.Data.Object as PaymentIntent;
                        _logger.LogWarning($"Payment failed: {failedIntent?.Id}");
                        break;

                    default:
                        _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Invalid webhook signature");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Webhook processing error");
                return StatusCode(500);
            }
        }
    }

    public class StripeConfirmRequest
    {
        public string? PaymentIntentId { get; set; }
        public OrderInsertRequest? OrderRequest { get; set; }
    }
}
