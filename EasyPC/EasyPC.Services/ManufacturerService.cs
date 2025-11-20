using EasyPC.Model.Requests.ManufacturerRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.ManufacturerStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class ManufacturerService : BaseService<Model.Manufacturer, ManufacturerSearchObjects, ManufacturerInsertRequest, ManufacturerUpdateRequest, Manufacturer, BaseManufacturerStateMachine>,IManufacturerService
    {
        public ManufacturerService(DatabaseContext context, IMapper mapper, BaseManufacturerStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<Manufacturer> ApplyFilter(IQueryable<Manufacturer> query, ManufacturerSearchObjects? searchObject)
        {
            if (!IsAdmin())
            {
                query = query.Where(x => x.StateMachine == StateNames.Active);
            }

            if (searchObject == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(searchObject.Name))
            {
                query = query.Where(r => r.Name.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject.ComponentType))
            {
                query = query.Where(r => r.ComponentType == searchObject.ComponentType);
            }
            return query;
        }
    }
}
