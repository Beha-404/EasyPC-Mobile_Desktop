using EasyPC.Model;
using EasyPC.Model.Requests.PcRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

public class PcController(IPcService service) : BaseController<PC, PcSearchObject, PcInsertRequest, PcUpdateRequest>(service)
{
    private readonly IPcService _service = service;

    [AllowAnonymous]
    [HttpGet("{id}/recommend")]
    public List<PC> Recommend(int id)
    {
        return _service.Recommend(id);
    }

    [Authorize]
    [HttpPost("insert-custom")]
    public IActionResult InsertCustomPc([FromBody] PcInsertRequest insertRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _service.Insert(insertRequest);
        return Ok(result);
    }
}
