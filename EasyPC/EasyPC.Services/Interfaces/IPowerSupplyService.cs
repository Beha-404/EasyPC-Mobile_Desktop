using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IPowerSupplyService : IBaseService<Model.PowerSupply,PowerSupplySearchObject,PowerSupplyInsertRequest,PowerSuplyUpdateRequest>
    {
    }
}
