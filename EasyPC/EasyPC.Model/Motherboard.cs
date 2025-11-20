using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Model;

public class Motherboard
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Socket { get; set; }
    public required int Price { get; set; }
    public bool SupportsOverclocking { get; set; }
    public string? StateMachine { get; set; }
    public int ManufacturerId { get; set; }
}
