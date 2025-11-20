using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.PowerSupplyRequests
{
    public class PowerSupplyInsertRequest
    {
        public required string Name { get; set; }
        public required string Power { get; set; }
        public required int Price { get; set; }
        public required int ManufacturerId { get; set; }
    }
}
