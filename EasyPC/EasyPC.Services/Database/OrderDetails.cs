using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int PcId { get; set; }
        [ForeignKey(nameof(PcId))]
        public PC? Pc { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }
    }
}
