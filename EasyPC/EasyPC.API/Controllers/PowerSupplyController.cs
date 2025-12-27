using EasyPC.Model;
using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers;

public class PowerSupplyController(IPowerSupplyService service) : BaseController<PowerSupply, PowerSupplySearchObject, PowerSupplyInsertRequest, PowerSuplyUpdateRequest>(service) { }
