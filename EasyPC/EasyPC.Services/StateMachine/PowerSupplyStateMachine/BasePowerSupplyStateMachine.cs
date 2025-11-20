using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine
{
    public class BasePowerSupplyStateMachine : BaseStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, Database.PowerSupply>
    {
        public BasePowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, Database.PowerSupply> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialPowerSupplyStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftPowerSupplyStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActivePowerSupplyStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenPowerSupplyStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
