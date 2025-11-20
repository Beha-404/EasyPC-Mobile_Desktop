using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.CaseStateMachine
{
    public class BaseCaseStateMachine : BaseStateMachine<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>
    {
        public BaseCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

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
}
