using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine;

public class InitialMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : InitialStateMachine<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Motherboard>(context, mapper, serviceProvider) { }
