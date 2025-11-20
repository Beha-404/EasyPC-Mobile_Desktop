using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Model;

public class Processor
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Socket { get; set; }
    public required int Price { get; set; }
    public required int CoreCount { get; set; }
    public required int ThreadCount { get; set; }
    public string? StateMachine { get; set; }
    public int ManufacturerId { get; set; }
}
