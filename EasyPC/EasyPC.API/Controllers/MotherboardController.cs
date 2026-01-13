using EasyPC.Model;
using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers;

public class MotherboardController(IMotherboardService service) : BaseController<Motherboard, MotherboardSearchObject, MotherboardInsertRequest, MotherboardUpdateRequest>(service) { }
