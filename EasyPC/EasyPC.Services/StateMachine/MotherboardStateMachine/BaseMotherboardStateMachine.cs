using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine;
public class BaseMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : BaseStateMachine<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard>(context, mapper, serviceProvider)
{
    public override IBaseStateMachine<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard> NextState(string state)
    {
        return state switch
        {
            "initial" => _serviceProvider.GetRequiredService<InitialMotherboardStateMachine>(),
            "draft" => _serviceProvider.GetRequiredService<DraftMotherboardStateMachine>(),
            "active" => _serviceProvider.GetRequiredService<ActiveMotherboardStateMachine>(),
            "hidden" => _serviceProvider.GetRequiredService<HiddenMotherboardStateMachine>(),
            _ => throw new NotImplementedException(),
        };
    }
}
