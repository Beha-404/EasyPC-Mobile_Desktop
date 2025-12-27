using EasyPC.Model;
using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IManufacturerService
{
    public PagedResult<Manufacturer> GetAll(ManufacturerSearchObjects search);
    public Manufacturer? GetById(int id);
    public Manufacturer? Insert(ManufacturerInsertRequest insertRequest);
    public Manufacturer? Update(int id, ManufacturerUpdateRequest updateRequest);
}
