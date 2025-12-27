using EasyPC.Model;
using EasyPC.Model.Requests.PcRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IPcService : IBaseService<PC, PcSearchObject, PcInsertRequest, PcUpdateRequest>
{
    List<PC> Recommend(int id);
}
