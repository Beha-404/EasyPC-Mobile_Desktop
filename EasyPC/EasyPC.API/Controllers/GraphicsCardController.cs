using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers
{
    public class GraphicsCardController : BaseController<Model.GraphicsCard,GraphicsCardSearchObject, GraphicsCardInsertRequest,GraphicsCardUpdateRequest>
    {
        public GraphicsCardController(IGraphicsCardService service) : base(service)
        {
        }
    }
}
