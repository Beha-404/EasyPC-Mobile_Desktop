using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Services.Database;
using EasyPC.Services.StateMachine.GraphicsCardStateMachine;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.GraphicsCard;

public class BaseGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : BaseStateMachine<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>(context, mapper, serviceProvider)
{
    public override IBaseStateMachine<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard> NextState(string state)
    {
        return state switch
        {
            "initial" => _serviceProvider.GetRequiredService<InitialGraphicsCardStateMachine>(),
            "draft" => _serviceProvider.GetRequiredService<DraftGraphicsCardStateMachine>(),
            "active" => _serviceProvider.GetRequiredService<ActiveGraphicsCardStateMachine>(),
            "hidden" => _serviceProvider.GetRequiredService<HiddenGraphicsCardStateMachine>(),
            _ => throw new NotImplementedException(),
        };
    }
}
