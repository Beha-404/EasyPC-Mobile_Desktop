using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine
{
    public class ActiveRamStateMachine : ActiveMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Database.Ram>
    {
        public ActiveRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        
    }
}
