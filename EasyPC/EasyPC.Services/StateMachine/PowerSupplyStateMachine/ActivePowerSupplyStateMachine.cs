using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine
{
    public class ActivePowerSupplyStateMachine : ActiveMachineState<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, Database.PowerSupply>
    {
        public ActivePowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
