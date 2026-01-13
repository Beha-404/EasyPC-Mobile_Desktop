using EasyPC.Model.Requests.PowerSupplyRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PowerSupplyStateMachine;

public class DraftPowerSupplyStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.PowerSupply, PowerSupplyInsertRequest, PowerSuplyUpdateRequest, PowerSupply>(context, mapper, serviceProvider) { }
