using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine
{
    public class ActiveMotherboardStateMachine : ActiveMachineState<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Database.Motherboard>
    {
        public ActiveMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
