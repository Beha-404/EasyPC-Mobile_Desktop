using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine
{
    public class HiddenProcessorStateMachine : HiddenMachineState<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Database.Processor>
    {
        public HiddenProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
