using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.CaseStateMachine
{
    public class HiddenCaseStateMachine : HiddenMachineState<Model.Case, CaseInsertRequest, CaseUpdateRequest, Database.Case>
    {
        public HiddenCaseStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
