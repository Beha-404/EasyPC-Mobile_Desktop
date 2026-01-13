using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPC.Services.StateMachine.CaseStateMachine;

public class BaseCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : BaseStateMachine<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>(context, mapper, serviceProvider)
{
    public override IBaseStateMachine<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case> NextState(string state)
    {
        return state switch
        {
            "initial" => _serviceProvider.GetRequiredService<InitialCaseStateMachine>(),
            "draft" => _serviceProvider.GetRequiredService<DraftCaseStateMachine>(),
            "active" => _serviceProvider.GetRequiredService<ActiveCaseStateMachine>(),
            "hidden" => _serviceProvider.GetRequiredService<HiddenCaseStateMachine>(),
            _ => throw new NotImplementedException(),
        };
    }
}
