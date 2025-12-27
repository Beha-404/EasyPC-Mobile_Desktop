using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine;

public class HiddenPowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : HiddenMachineState<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply>(context, mapper, serviceProvider) { }
