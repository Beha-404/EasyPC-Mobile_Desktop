using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class Order
{
    [Key]
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Details { get; set; }

    public int TotalPrice { get; set; }


    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
}
