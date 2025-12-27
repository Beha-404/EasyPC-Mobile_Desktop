using EasyPC.Model;
using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IPowerSupplyService : IBaseService<PowerSupply, PowerSupplySearchObject, PowerSupplyInsertRequest, PowerSuplyUpdateRequest> { }
