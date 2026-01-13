using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine;

public class HiddenMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : HiddenMachineState<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard>(context, mapper, serviceProvider) { }
