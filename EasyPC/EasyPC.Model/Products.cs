namespace EasyPC.Model
{
    public class Products
    {
        public List<Processor> Processors { get; set; } = new List<Processor>();
        public List<GraphicsCard> GraphicsCards { get; set; } = new List<GraphicsCard>();
        public List<Ram> Rams { get; set; } = new List<Ram>();
        public List<Case> Cases { get; set; } = new List<Case>();
        public List<PowerSupply> PowerSupplies { get; set; } = new List<PowerSupply>();
        public List<Motherboard> MotherBoards { get; set; } = new List<Motherboard>();
        public List<PcType> PcType { get; set; } = new List<PcType>();
        public List<PC> Pcs { get; set; } = new List<PC>();
        public List<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
    }
}