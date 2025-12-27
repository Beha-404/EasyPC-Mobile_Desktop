using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.GraphicsCardStateMachine;

public class DraftGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>(context, mapper, serviceProvider) { }
