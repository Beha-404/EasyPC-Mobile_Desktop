using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.Requests.MotherboardRequests;
using EasyPC.Services.Database;
using MapsterMapper;


namespace EasyPC.Services.StateMachine.MotherboardStateMachine
{
    public class InitialMotherboardStateMachine : InitialStateMachine<Model.Motherboard, MotherboardInsertRequest, MotherboardUpdateRequest, Database.Motherboard>
    {
        public InitialMotherboardStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
