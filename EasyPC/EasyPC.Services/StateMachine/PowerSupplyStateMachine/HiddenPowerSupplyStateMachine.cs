using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine
{
    public class HiddenPowerSupplyStateMachine : HiddenMachineState<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, Database.PowerSupply>
    {
        public HiddenPowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
