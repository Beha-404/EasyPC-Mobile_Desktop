using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.CaseStateMachine;

public class InitialCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : InitialStateMachine<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>(context, mapper, serviceProvider) { }
