using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.PcRequests
{
    public class PcInsertRequest
    {
        public required string Name { get; set; }
        public required int PcTypeId { get; set; }
        public required int ProcessorId { get; set; }
        public required int RamId { get; set; }
        public required int CaseId { get; set; }
        public required int MotherBoardId { get; set; }
        public required int PsuId { get; set; }
        public required int GraphicsCardId { get; set; }
    }
}
