using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine
{
    public class InitialProcessorStateMachine : InitialStateMachine<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Database.Processor>
    {
        public InitialProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
