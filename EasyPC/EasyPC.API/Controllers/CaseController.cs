using EasyPC.Model;
using EasyPC.Model.Requests.CaseRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EasyPC.API.Controllers;

[Authorize]
public class CaseController(ICaseService service) : BaseController<Case, CaseSearchObject, CaseInsertRequest, CaseUpdateRequest>(service) { }
