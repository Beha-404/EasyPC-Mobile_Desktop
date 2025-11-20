namespace EasyPC.Model.Requests.PcRequests
{
    public class PcUpdateRequest
    {
        public string? Name { get; set; }
        public int PcTypeId { get; set; }
        public int? Rating { get; set; }
        public string? Picture { get; set; }
        public int ProcessorId { get; set; }
        public int RamId { get; set; }
        public int CaseId { get; set; }
        public int MotherBoardId { get; set; }
        public int PowerSupplyId { get; set; }
        public int GraphicsCardId { get; set; }
    }
}
