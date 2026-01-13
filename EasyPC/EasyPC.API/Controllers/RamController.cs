using EasyPC.Model.Requests.RamRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers;

public class RamController(IRamService service) : BaseController<Model.Ram, RamSearchObject, RamInsertRequest, RamUpdateRequest>(service) { }
