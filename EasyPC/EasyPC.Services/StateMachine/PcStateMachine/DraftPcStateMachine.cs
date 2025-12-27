using EasyPC.Model.Requests.PcRequests;
using EasyPC.Services.Database;
using MapsterMapper;

namespace EasyPC.Services.StateMachine.PcStateMachine;

public class DraftPcStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : DraftMachineState<Model.PC, PcInsertRequest, PcUpdateRequest, PC>(context, mapper, serviceProvider)
{
}
