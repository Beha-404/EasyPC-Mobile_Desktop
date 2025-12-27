using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine;

public class ActiveMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard>(context, mapper, serviceProvider) { }
