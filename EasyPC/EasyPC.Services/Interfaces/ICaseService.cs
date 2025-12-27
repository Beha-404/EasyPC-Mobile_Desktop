using EasyPC.Model;
using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces
{
    public interface ICaseService : IBaseService<Case, CaseSearchObject, CaseInsertRequest, CaseUpdateRequest> { }
}
