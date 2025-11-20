namespace EasyPC.Model.Requests.OrderRequests
{
    public class OrderInsertRequest
    {
        public string? PaymentMethod { get; set; }
        public int UserId { get; set; }
        public List<OrderDetailsInsertRequest> OrderDetails { get; set; } = new();
    }
}
