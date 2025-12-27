using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine;

public class HiddenProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : HiddenMachineState<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Processor>(context, mapper, serviceProvider) { }
