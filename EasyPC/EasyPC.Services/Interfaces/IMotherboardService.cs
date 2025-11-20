using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IMotherboardService : IBaseService<Model.Motherboard,MotherboardSearchObject,MotherboardInsertRequest,MotherboardUpdateRequest>
    {
    }
}
