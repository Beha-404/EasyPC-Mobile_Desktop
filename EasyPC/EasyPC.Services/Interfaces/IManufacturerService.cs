using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IManufacturerService : IBaseService<Model.Manufacturer,ManufacturerSearchObjects,ManufacturerInsertRequest,ManufacturerUpdateRequest>
    {
    }
}
