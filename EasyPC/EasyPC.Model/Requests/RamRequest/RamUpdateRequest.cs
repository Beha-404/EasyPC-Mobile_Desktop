using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.RamRequests
{
    public class RamUpdateRequest
    {
        public  string? Name { get; set; }
        public  int Price { get; set; }
        public  string? Speed { get; set; }
        public  int ManufacturerId { get; set; }
    }
}
