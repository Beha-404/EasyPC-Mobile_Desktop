using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.ProcessorRequests
{
    public class ProcessorUpdateRequest
    {
        public  string? Name { get; set; }
        public  string? Socket { get; set; }
        public  int Price { get; set; }
        public  int CoreCount { get; set; }
        public  int ThreadCount { get; set; }
        public  int ManufacturerId { get; set; }
    }
}
