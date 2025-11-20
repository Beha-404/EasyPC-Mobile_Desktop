using EasyPC.Model.Requests.RamRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.RamStateMachine
{
    public class DraftRamStateMachine : DraftMachineState<Model.Ram, RamInsertRequest, RamUpdateRequest, Database.Ram>
    {
        public DraftRamStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
