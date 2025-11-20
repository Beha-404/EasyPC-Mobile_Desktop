using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class GraphicsCard
{
    public int Id { get; set; }
    [MinLength(3)]
    public required string Name { get; set; }
    [MinLength(3)]
    public required string VRAM { get; set; }
    public required int Price { get; set; }
    public required string Type { get; set; } = "GPU";
    public string? StateMachine { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

}
