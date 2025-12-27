using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine;

public class InitialRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : InitialStateMachine<Model.Ram, RamInsertRequest, RamUpdateRequest, Ram>(context, mapper, serviceProvider) { }
