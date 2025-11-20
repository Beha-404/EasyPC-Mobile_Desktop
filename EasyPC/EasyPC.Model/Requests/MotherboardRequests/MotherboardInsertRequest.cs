using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.MotherboardRequests
{
    public class MotherboardInsertRequest
    {
        public required string Name { get; set; }
        public required string Socket { get; set; }
        public required int Price { get; set; }
        public required int ManufacturerId { get; set; }
    }
}
