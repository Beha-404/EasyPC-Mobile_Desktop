using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine;

public class ActiveProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Processor>(context, mapper, serviceProvider) { }
