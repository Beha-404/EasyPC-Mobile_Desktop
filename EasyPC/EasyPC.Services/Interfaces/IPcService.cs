using EasyPC.Model.Requests.PcRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IPcService : IBaseService<Model.PC,PcSearchObject,PcInsertRequest,PcUpdateRequest>
    {
        List<Model.PC> Recommend(int id);
    }
}
