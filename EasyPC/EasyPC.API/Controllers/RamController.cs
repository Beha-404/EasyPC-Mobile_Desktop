using EasyPC.Model.Requests.RamRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class RamController : BaseController<Model.Ram,RamSearchObject, RamInsertRequest,RamUpdateRequest>
    {
        public RamController(IRamService service) : base(service)
        {
        }
    }
}
