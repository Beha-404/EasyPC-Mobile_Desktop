namespace EasyPC.Model.Messages
{
    public class OrderEmailMessage
    {
        public int OrderId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public List<OrderItemDetail> OrderItems { get; set; } = new();
    }

    public class OrderItemDetail
    {
        public string PcName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
