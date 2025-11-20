namespace EasyPC.Model.SearchObjects
{
    public class PcSearchObject : BaseSearchObject
    {
        public string? Name { get; set; }
        public int PcTypeId { get; set; }
        public int? Price { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? Rating { get; set; }
        public bool? Available { get; set; }
        public string? StateMachine { get; set; }
        public int? ProcessorManufacturerId { get; set; }
        public int? GraphicsCardManufacturerId { get; set; }
        public int? RamManufacturerId { get; set; }
        public int? MotherBoardManufacturerId { get; set; }
        public int? PowerSupplyManufacturerId { get; set; }
        public int? CaseManufacturerId { get; set; }
    }
}
