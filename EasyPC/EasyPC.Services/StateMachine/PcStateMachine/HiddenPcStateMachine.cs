using EasyPC.Model.Requests.PcRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PcStateMachine;

public class HiddenPcStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : HiddenMachineState<Model.PC, PcInsertRequest, PcUpdateRequest, PC>(context, mapper, serviceProvider) { }
