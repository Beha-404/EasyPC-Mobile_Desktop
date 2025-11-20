using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.ManufacturerStateMachine
{
    public class DraftManufacturerStateMachine : DraftMachineState<Model.Manufacturer, ManufacturerInsertRequest, ManufacturerUpdateRequest, Database.Manufacturer>
    {
        public DraftManufacturerStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
