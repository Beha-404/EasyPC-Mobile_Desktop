using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IProcessorService : IBaseService<Model.Processor,ProcessorSearchObject,ProcessorInsertRequest,ProcessorUpdateRequest>
    {
    }
}
