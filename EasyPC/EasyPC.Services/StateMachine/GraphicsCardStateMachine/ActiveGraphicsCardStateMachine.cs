using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.GraphicsCardStateMachine;

public class ActiveGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>(context, mapper, serviceProvider) { }
