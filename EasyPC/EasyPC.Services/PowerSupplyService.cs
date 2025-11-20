using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.PowerSupplyStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class PowerSupplyService : BaseService<Model.PowerSupply, PowerSupplySearchObject,PowerSupplyInsertRequest,PowerSuplyUpdateRequest, Database.PowerSupply, BasePowerSupplyStateMachine>,IPowerSupplyService 
    {
        public PowerSupplyService(DatabaseContext context, IMapper mapper, BasePowerSupplyStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<PowerSupply> ApplyFilter(IQueryable<PowerSupply> query, PowerSupplySearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Name))
            {
                query = query.Where(r => r.Name!.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject!.Power))
            {
                query = query.Where(r => r.Power!.Contains(searchObject.Power));
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
