using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine;

public class DraftRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Ram>(context, mapper, serviceProvider) { }
