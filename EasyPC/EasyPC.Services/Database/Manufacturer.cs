using System.ComponentModel.DataAnnotations;

namespace EasyPC.Services.Database;
public class Manufacturer
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ComponentType { get; set; } = null!;
}