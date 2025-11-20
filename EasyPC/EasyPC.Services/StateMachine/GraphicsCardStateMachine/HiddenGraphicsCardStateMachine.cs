using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.GraphicsCardStateMachine
{
    public class HiddenGraphicsCardStateMachine : HiddenMachineState<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>
    {
        public HiddenGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
