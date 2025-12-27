using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;


namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine;

public class InitialPowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : InitialStateMachine<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply>(context, mapper, serviceProvider) { }
