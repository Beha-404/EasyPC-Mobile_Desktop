using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class PowerSupplyController : BaseController<Model.PowerSupply,PowerSupplySearchObject, PowerSupplyInsertRequest,PowerSuplyUpdateRequest>
    {
        public PowerSupplyController(IPowerSupplyService service) : base(service)
        {
        }
    }
}
