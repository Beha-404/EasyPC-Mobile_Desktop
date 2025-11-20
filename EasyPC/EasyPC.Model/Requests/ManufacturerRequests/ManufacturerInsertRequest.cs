using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.ManufacturerRequests
{
    public class ManufacturerInsertRequest
    {
        public required string Name { get; set; }
        public required string ComponentType { get; set; }
    }
}
