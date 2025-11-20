using EasyPC.Model.Requests.RamRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.RamStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class RamService : BaseService<Model.Ram,RamSearchObject, RamInsertRequest,RamUpdateRequest, Database.Ram, BaseRamStateMachine>,IRamService
    {
        public RamService(DatabaseContext context, IMapper mapper, BaseRamStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<Ram> ApplyFilter(IQueryable<Ram> query, RamSearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Name))
            {
                query = query.Where(r => r.Name!.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject!.Speed))
            {
                query = query.Where(r => r.Speed!.Contains(searchObject.Speed));
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
