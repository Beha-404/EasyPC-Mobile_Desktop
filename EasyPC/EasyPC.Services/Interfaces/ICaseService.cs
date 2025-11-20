using EasyPC.Model;
using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface ICaseService : IBaseService<Case, CaseSearchObject,CaseInsertRequest,CaseUpdateRequest>
    {
    }
}
