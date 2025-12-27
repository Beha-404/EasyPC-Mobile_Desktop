using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.ProcessorStateMachine;

public class InitialProcessorStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : InitialStateMachine<Model.Processor, ProcessorInsertRequest, ProcessorUpdateRequest, Processor>(context, mapper, serviceProvider) { }
