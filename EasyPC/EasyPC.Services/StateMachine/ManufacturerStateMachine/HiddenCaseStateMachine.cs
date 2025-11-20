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
    public class HiddenManufacturerStateMachine : HiddenMachineState<Model.Manufacturer, ManufacturerInsertRequest, ManufacturerUpdateRequest, Database.Manufacturer>
    {
        public HiddenManufacturerStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }
    }
}
