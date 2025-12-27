using System.ComponentModel.DataAnnotations.Schema;

namespace EasyPC.Services.Database;

public class PC
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Type { get; set; } = "PC";

    public int? Price { get; set; }

    public double? AverageRating { get; set; }

    public int? RatingCount { get; set; }

    public bool Available { get; set; } = true;

    public string? Picture { get; set; }

    public string? StateMachine { get; set; }

    [ForeignKey(nameof(ProcessorId))]
    public int ProcessorId { get; set; }
    public Processor? Processor { get; set; }

    [ForeignKey(nameof(RamId))]
    public int RamId { get; set; }
    public Ram? Ram { get; set; }

    [ForeignKey(nameof(CaseId))]
    public int CaseId { get; set; }
    public Case? Case { get; set; }

    [ForeignKey(nameof(MotherBoardId))]
    public int MotherBoardId { get; set; }
    public virtual Motherboard? MotherBoard { get; set; }

    [ForeignKey(nameof(PowerSupplyId))]
    public int PowerSupplyId { get; set; }
    public PowerSupply? PowerSupply { get; set; }

    [ForeignKey(nameof(GraphicsCardId))]
    public int GraphicsCardId { get; set; }
    public GraphicsCard? GraphicsCard { get; set; }

    [ForeignKey(nameof(PcTypeId))]
    public int PcTypeId { get; set; }
    public PcType? PcType { get; set; }

    public virtual ICollection<Rating>? Ratings { get; set; }
    public virtual ICollection<OrderDetails>? OrderDetails { get; set; }

    [NotMapped]
    public int CalculatedPrice
    {
        get
        {
            return
                (Processor?.Price ?? 0) +
                (GraphicsCard?.Price ?? 0) +
                (Ram?.Price ?? 0) +
                (PowerSupply?.Price ?? 0) +
                (Case?.Price ?? 0) +
                (MotherBoard?.Price ?? 0);
        }
    }
}
