namespace EasyPC.Model.Requests.OrderRequests;

using EasyPC.Model.Requests.OrderDetailsRequests;

public class OrderInsertRequest
{
    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; } = "Pending";

    public string? PayPalOrderId { get; set; }

    public string? StripePaymentIntentId { get; set; }

    public int UserId { get; set; }

    public List<OrderDetailsInsertRequest> OrderDetails { get; set; } = new();
}
