using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.RamRequests
{
    public class RamInsertRequest
    {
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required string Speed { get; set; }
        public required int ManufacturerId { get; set; }
    }
}
