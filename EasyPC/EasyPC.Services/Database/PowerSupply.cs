using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class PowerSupply
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Power { get; set; }
    public required int Price { get; set; }
    public required string Type { get; set; } = "PSU";
    public string? StateMachine { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

}
