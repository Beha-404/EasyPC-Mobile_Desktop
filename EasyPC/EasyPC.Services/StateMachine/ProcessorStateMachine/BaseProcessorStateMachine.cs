using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine
{
    public class BaseProcessorStateMachine : BaseStateMachine<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Database.Processor>
    {
        public BaseProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Database.Processor> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialProcessorStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftProcessorStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActiveProcessorStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenProcessorStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
