using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.RamStateMachine
{
    public class BaseRamStateMachine : BaseStateMachine<Model.Ram, RamInsertRequest, RamUpdateRequest, Database.Ram>
    {
        public BaseRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.Ram, RamInsertRequest, RamUpdateRequest, Database.Ram> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialRamStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftRamStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActiveRamStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenRamStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
