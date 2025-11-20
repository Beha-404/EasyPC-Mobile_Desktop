namespace EasyPC.Model
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int? Rating { get; set; }
        public int OrderId { get; set; }
        public int PcId { get; set; }
        public PC? Pc { get; set; }
    }
}
