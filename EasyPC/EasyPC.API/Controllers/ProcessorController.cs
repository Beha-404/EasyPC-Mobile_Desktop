using EasyPC.Model;
using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;

namespace EasyPC.API.Controllers;

public class ProcessorController(IProcessorService service) : BaseController<Processor, ProcessorSearchObject, ProcessorInsertRequest, ProcessorUpdateRequest>(service) { }
