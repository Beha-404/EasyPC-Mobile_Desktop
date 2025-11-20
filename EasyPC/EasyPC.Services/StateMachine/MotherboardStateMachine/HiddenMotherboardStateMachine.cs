using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.MotherboardStateMachine
{
    public class HiddenMotherboardStateMachine : HiddenMachineState<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Database.Motherboard>
    {
        public HiddenMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
