using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine;

public class BasePowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : BaseStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply>(context, mapper, serviceProvider)
{
    public override IBaseStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply> NextState(string state)
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
