using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class MotherboardController : BaseController<Model.Motherboard, MotherboardSearchObject,MotherboardInsertRequest,MotherboardUpdateRequest>
    {
        public MotherboardController(IMotherboardService service) : base(service)
        {
        }
    }
}
