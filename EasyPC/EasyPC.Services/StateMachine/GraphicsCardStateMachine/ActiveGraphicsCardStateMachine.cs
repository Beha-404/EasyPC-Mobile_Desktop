using EasyPC.Model.Requests.GraphicsCardRequests;
using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.GraphicsCardStateMachine
{
    public class ActiveGraphicsCardStateMachine : ActiveMachineState<Model.GraphicsCard, GraphicsCardInsertRequest, GraphicsCardUpdateRequest, Database.GraphicsCard>
    {
        public ActiveGraphicsCardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
