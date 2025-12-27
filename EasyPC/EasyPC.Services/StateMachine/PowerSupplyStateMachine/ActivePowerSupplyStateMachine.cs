using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine;

public class ActivePowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply>(context, mapper, serviceProvider) { }
