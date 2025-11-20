namespace EasyPC.Model;

public class Case
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public string? FormFactor { get; set; }
    public Manufacturer? Manufacturer { get; set; }
    public int ManufacturerId { get; set; }
    public string? StateMachine { get; set; }
}
