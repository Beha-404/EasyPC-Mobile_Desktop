using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class Ram
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; } = "RAM";
    public required int Price { get; set; }
    public required string Speed { get; set; }
    public string? StateMachine { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
}
