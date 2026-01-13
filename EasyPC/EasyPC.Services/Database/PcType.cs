using System.ComponentModel.DataAnnotations;

namespace EasyPC.Services.Database;

public class PcType
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}