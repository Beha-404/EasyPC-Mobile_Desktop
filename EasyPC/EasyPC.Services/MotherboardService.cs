using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.MotherboardStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class MotherboardService : BaseService<Model.Motherboard, MotherboardSearchObject,MotherboardInsertRequest,MotherboardUpdateRequest, Database.Motherboard, BaseMotherboardStateMachine>,IMotherboardService
    {
        public MotherboardService(DatabaseContext context, IMapper mapper, BaseMotherboardStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<Motherboard> ApplyFilter(IQueryable<Motherboard> query, MotherboardSearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Name))
            {
                query = query.Where(r => r.Name!.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject!.Socket))
            {
                query = query.Where(r => r.Socket!.Contains(searchObject.Socket));
            }

            if (searchObject.Price.HasValue)
            {
                query = query.Where(r => r.Price == searchObject.Price);
            }
            if (searchObject.SupportsOverclocking.HasValue)
            {
                query = query.Where(r => r.SupportsOverclocking == searchObject.SupportsOverclocking);
            }

            if (!IsAdmin())
            {
                query = query.Where(x => x.StateMachine == StateNames.Active);
            }

            return query;
        }
    }
}
