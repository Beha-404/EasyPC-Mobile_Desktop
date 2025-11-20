using EasyPC.Model;
using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class ManufacturerController : BaseController<Manufacturer, ManufacturerSearchObjects,ManufacturerInsertRequest,ManufacturerUpdateRequest>
    {
        public ManufacturerController(IManufacturerService service) : base(service)
        {
        }
    }
}
