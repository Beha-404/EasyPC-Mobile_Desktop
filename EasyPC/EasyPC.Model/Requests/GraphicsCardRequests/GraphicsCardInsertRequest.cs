using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.GraphicsCardRequests
{
    public class GraphicsCardInsertRequest
    {
        public required string Name { get; set; }
        public required string VRAM { get; set; }
        public required int Price { get; set; }
        public required int ManufacturerId { get; set; }
    }
}
