using EasyPC.Model.Requests.RamRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Model;

namespace EasyPC.Services.Interfaces;

public interface IRamService : IBaseService<Ram, RamSearchObject, RamInsertRequest, RamUpdateRequest> { }
