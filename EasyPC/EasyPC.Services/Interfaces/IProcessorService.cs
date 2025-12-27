using EasyPC.Model;
using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IProcessorService : IBaseService<Processor, ProcessorSearchObject, ProcessorInsertRequest, ProcessorUpdateRequest> { }
