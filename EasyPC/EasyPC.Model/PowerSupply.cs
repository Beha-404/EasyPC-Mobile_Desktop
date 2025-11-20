using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Model;

public class PowerSupply
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Power { get; set; }
    public required int Price { get; set; }
    public string? StateMachine { get; set; }
    public int ManufacturerId { get; set; }
}
