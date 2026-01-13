using EasyPC.Model;
using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers;

public class GraphicsCardController(IGraphicsCardService service) : BaseController<GraphicsCard, GraphicsCardSearchObject, GraphicsCardInsertRequest, GraphicsCardUpdateRequest>(service) { }
