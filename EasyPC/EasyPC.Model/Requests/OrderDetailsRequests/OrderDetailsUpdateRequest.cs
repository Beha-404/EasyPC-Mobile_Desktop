namespace EasyPC.Model.Requests.OrderRequests
{
    public class OrderDetailsUpdateRequest
    {
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public string? Details { get; set; }
        public int TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
