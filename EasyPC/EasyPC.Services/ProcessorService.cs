using EasyPC.Model.Requests.ProcessorRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.ProcessorStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services
{
    public class ProcessorService : BaseService<Model.Processor, ProcessorSearchObject,ProcessorInsertRequest,ProcessorUpdateRequest, Database.Processor, BaseProcessorStateMachine>,IProcessorService
    {
        public ProcessorService(DatabaseContext context, IMapper mapper, BaseProcessorStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper, stateMachine, httpContextAccessor)
        {
        }

        public override IQueryable<Processor> ApplyFilter(IQueryable<Processor> query, ProcessorSearchObject? searchObject)
        {
            if (!string.IsNullOrEmpty(searchObject!.Name))
            {
                query = query.Where(r => r.Name!.Contains(searchObject.Name));
            }
            if (!string.IsNullOrEmpty(searchObject!.Socket))
            {
                query = query.Where(r => r.Socket!.Contains(searchObject.Socket));
            }

            if (searchObject.Price.HasValue)
            {
                query = query.Where(r => r.Price == searchObject.Price);
            }

            if (searchObject.ThreadCount.HasValue)
            {
                query = query.Where(r => r.ThreadCount == searchObject.ThreadCount);
            }

            if (searchObject.CoreCount.HasValue)
            {
                query = query.Where(r => r.CoreCount == searchObject.CoreCount);
            }

            if (!IsAdmin())
            {
                query = query.Where(x => x.StateMachine == StateNames.Active);
            }

            return query;
        }
    }
}
