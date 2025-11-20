using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.GraphicsCardRequests
{
    public class GraphicsCardUpdateRequest
    {
        public  string? Name { get; set; }
        public  string? VRAM { get; set; }
        public  int Price { get; set; }
        public  int ManufacturerId { get; set; }
    }
}
