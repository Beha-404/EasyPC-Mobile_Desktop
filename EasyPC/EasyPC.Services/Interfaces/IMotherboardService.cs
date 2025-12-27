using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IMotherboardService : IBaseService<Model.Motherboard, MotherboardSearchObject, MotherboardInsertRequest, MotherboardUpdateRequest> { }
