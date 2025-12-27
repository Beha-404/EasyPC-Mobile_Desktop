using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.CaseStateMachine;

public class ActiveCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : ActiveMachineState<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>(context, mapper, serviceProvider) { }
