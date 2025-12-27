using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine
{
    public class DraftProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Processor>(context, mapper, serviceProvider) { }
}
