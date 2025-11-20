namespace EasyPC.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public int UserId { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}