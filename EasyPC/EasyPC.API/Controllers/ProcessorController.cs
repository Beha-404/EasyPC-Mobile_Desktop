using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class ProcessorController : BaseController<Model.Processor, ProcessorSearchObject,ProcessorInsertRequest,ProcessorUpdateRequest>
    {
        public ProcessorController(IProcessorService service) : base(service)
        {
        }
    }
}
