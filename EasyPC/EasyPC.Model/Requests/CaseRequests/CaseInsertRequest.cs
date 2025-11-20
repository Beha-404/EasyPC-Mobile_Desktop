using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.CaseRequests
{
    public class CaseInsertRequest
    {
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required string FormFactor { get; set; }
        public required int ManufacturerId { get; set; }
    }
}
