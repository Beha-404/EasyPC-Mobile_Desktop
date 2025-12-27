using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine;

public class ActiveRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Ram>(context, mapper, serviceProvider) { }
