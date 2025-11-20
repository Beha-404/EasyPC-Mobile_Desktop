using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.Interfaces
{
    public interface IGraphicsCardService : IBaseService<Model.GraphicsCard, GraphicsCardSearchObject,GraphicsCardInsertRequest,GraphicsCardUpdateRequest>
    {
    }
}
