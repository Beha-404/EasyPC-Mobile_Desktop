using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine;

public class HiddenRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : HiddenMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Ram>(context, mapper, serviceProvider) { }
