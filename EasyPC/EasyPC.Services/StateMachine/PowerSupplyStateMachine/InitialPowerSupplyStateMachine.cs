using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;


namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine
{
    public class InitialPowerSupplyStateMachine : InitialStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, Database.PowerSupply>
    {
        public InitialPowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
