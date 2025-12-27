using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine;

public class DraftMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard>(context, mapper, serviceProvider) { }
