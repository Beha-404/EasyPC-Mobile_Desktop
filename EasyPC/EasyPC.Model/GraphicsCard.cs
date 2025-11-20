using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Model;

public class GraphicsCard
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string VRAM { get; set; }
    public required int Price { get; set; }
    public string? StateMachine { get; set; }
    public int ManufacturerId { get; set; }
}
