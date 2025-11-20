using EasyPC.Model.Requests.PcRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.PcStateMachine
{
    public class BasePcStateMachine : BaseStateMachine<Model.PC, PcInsertRequest, PcUpdateRequest, Database.PC>
    {
        public BasePcStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.PC, PcInsertRequest, PcUpdateRequest, Database.PC> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialPcStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftPcStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActivePcStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenPcStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
