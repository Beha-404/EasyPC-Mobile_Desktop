using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.CaseStateMachine;

public class DraftCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>(context, mapper, serviceProvider) { }
