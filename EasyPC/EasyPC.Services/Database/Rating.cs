using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EasyPC.Services.Database;

public class Rating
{
    public int Id { get; set; }
    [Range(1,5)]
    public int RatingValue { get; set; }
    public int UserId { get; set; }
    public int PcId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [ForeignKey("PcId")]
    public PC? PC { get; set; }
}
