using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine
{
    public class HiddenRamStateMachine : HiddenMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Database.Ram>
    {
        public HiddenRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
