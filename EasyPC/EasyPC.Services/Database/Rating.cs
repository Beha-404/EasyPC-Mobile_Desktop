using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Range(1, 5)]
    public int RatingValue { get; set; }

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey(nameof(PcId))]
    public int PcId { get; set; }
    public PC? PC { get; set; }
}
