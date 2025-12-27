using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class OrderDetails
{
    [Key]
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int UnitPrice { get; set; }

    [ForeignKey(nameof(PcId))]
    public int PcId { get; set; }
    public PC? Pc { get; set; }

    [ForeignKey(nameof(OrderId))]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
}
