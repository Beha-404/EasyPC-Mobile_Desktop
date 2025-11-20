using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class Case
{
    public int Id { get; set; }
    [MinLength(5)]
    public required string Name { get; set; }
    [MinLength(2)]
    public required string Type { get; set; } = "CASE";
    public required int Price { get; set; }
    public string? FormFactor { get; set; }
    public string? StateMachine { get; set; }

    [ForeignKey(nameof(Manufacturer))]
    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

}
