using EasyPC.Model;
using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces
{
    public interface IGraphicsCardService : IBaseService<GraphicsCard, GraphicsCardSearchObject, GraphicsCardInsertRequest, GraphicsCardUpdateRequest> { }
}
