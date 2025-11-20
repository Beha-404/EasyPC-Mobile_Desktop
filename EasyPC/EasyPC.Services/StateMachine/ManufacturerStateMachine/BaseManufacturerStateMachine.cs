using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Services.Database;
using EasyPC.Services.StateMachine.CaseStateMachine;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Services.StateMachine.ManufacturerStateMachine
{
    public class BaseManufacturerStateMachine : BaseStateMachine<Model.Manufacturer, ManufacturerInsertRequest, ManufacturerUpdateRequest, Database.Manufacturer>
    {
        public BaseManufacturerStateMachine(DatabaseContext context, IMapper mapper, IServiceProvider serviceProvider) : base(context, mapper, serviceProvider)
        {
        }

        public override IBaseStateMachine<Model.Manufacturer, ManufacturerInsertRequest, ManufacturerUpdateRequest, Database.Manufacturer> NextState(string state)
        {
            return state switch
            {
                "initial" => _serviceProvider.GetRequiredService<InitialManufacturerStateMachine>(),
                "draft" => _serviceProvider.GetRequiredService<DraftManufacturerStateMachine>(),
                "active" => _serviceProvider.GetRequiredService<ActiveManufacturerStateMachine>(),
                "hidden" => _serviceProvider.GetRequiredService<HiddenManufacturerStateMachine>(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
