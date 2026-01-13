using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Database;
using EasyPC.Services.Interfaces;
using EasyPC.Services.StateMachine.CaseStateMachine;
using MapsterMapper;
using Microsoft.AspNetCore.Http;

namespace EasyPC.Services;

public class CaseService(DatabaseContext context, IMapper mapper, BaseCaseStateMachine stateMachine, IHttpContextAccessor httpContextAccessor) : BaseService<Model.Case, CaseSearchObject, CaseInsertRequest, CaseUpdateRequest, Case, BaseCaseStateMachine>(context, mapper, stateMachine, httpContextAccessor), ICaseService
{
    public override IQueryable<Case> ApplyFilter(IQueryable<Case> query, CaseSearchObject? searchObject)
    {
        if (!string.IsNullOrEmpty(searchObject!.Name))
        {
            query = query.Where(r => r.Name!.Contains(searchObject.Name));
        }
        if (!string.IsNullOrEmpty(searchObject!.FormFactor))
        {
            query = query.Where(r => r.FormFactor!.Contains(searchObject.FormFactor));
        }

        if (searchObject.Price.HasValue)
        {
            query = query.Where(r => r.Price == searchObject.Price);
        }

        if (!IsAdmin())
        {
            query = query.Where(x => x.StateMachine == StateNames.Active);
        }

        return query;
    }
}