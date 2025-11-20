using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Services.Database;
using EasyPC.Services.StateMachine.CaseStateMachine;
using EasyPC.Services.StateMachine.GraphicsCardStateMachine;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.GraphicsCard
{
    public class BaseGraphicsCardStateMachine : BaseStateMachine<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>
    {
        public BaseGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialGraphicsCardStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftGraphicsCardStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActiveGraphicsCardStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenGraphicsCardStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
