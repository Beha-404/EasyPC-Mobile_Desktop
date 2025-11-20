using EasyPC.Model.Requests.PcRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PcStateMachine
{
    public class ActivePcStateMachine : ActiveMachineState<Model.PC, PcInsertRequest, PcUpdateRequest, Database.PC>
    {
        public ActivePcStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
