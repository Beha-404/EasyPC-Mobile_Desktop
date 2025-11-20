namespace EasyPC.Model;

public class PC
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Type { get; set; } = "PC";
    public int PcTypeId { get; set; }
    public PcType? PcType { get; set; }
    public int? Price { get; set; }
    public int? AverageRating { get; set; }
    public int? RatingCount { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public bool Available { get; set; } = true;
    public string? Picture { get; set; }
    public string? StateMachine { get; set; }
    public int ProcessorId { get; set; }
    public Processor? Processor { get; set; }
    public int RamId { get; set; }
    public Ram? Ram { get; set; }
    public int CaseId { get; set; }
    public Case? Case { get; set; }
    public int MotherBoardId { get; set; }
    public Motherboard? MotherBoard { get; set; }
    public int PsuId { get; set; }
    public PowerSupply? PowerSupply { get; set; }
    public int GraphicsCardId { get; set; }
    public GraphicsCard? GraphicsCard { get; set; }
}
