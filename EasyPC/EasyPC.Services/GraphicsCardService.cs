using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.GraphicsCard;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class GraphicsCardService : BaseService<Model.GraphicsCard,GraphicsCardSearchObject ,GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard,BaseGraphicsCardStateMachine>, IGraphicsCardService
    {
        public GraphicsCardService(DatabaseContext context, IMapper mapper, BaseGraphicsCardStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<GraphicsCard> ApplyFilter(IQueryable<GraphicsCard> query, GraphicsCardSearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Name))
            {
                query = query.Where(r => r.Name!.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject!.VRAM))
            {
                query = query.Where(r => r.VRAM!.Contains(searchObject.VRAM));
            }

            if (searchObject.Price.HasValue)
            {
                query = query.Where(r => r.Price == searchObject.Price);
            }

            if (!IsAdmin())
            {
                query = query.Where(x => x.StateMachine == StateNames.Active);
            }

            return query;
        }
    }
}
