namespace EasyPC.Model.Requests.OrderRequests
{
    public class OrderUpdateRequest
    {
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string? Status { get; set; }
    }
}
