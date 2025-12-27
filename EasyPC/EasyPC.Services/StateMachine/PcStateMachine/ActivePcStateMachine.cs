using EasyPC.Model.Requests.PcRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PcStateMachine;

public class ActivePcStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.PC, PcInsertRequest, PcUpdateRequest, Database.PC>(context, mapper, serviceProvider) { }
