using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseController<TModel, TSearch, TInsert, TUpdate>(IBaseService<TModel, TSearch, TInsert, TUpdate> service) : ControllerBase
{
    protected IBaseService<TModel, TSearch, TInsert, TUpdate> _service = service;


    [AllowAnonymous]
    [HttpGet("get")]
    public virtual Model.PagedResult<TModel> GetAll([FromQuery] TSearch search) => _service.GetAll(search);


    [AllowAnonymous]
    [HttpGet("get/{id}")]
    public virtual TModel? GetById(int id) => _service.GetById(id);


    [HttpGet("allowedActions/{id}")]
    public virtual List<string> AllowedActions(int id) => _service.AllowedActions(id);


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("activate/{id}")]
    public virtual TModel? Activate(int id) => _service.Activate(id);


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("edit/{id}")]
    public virtual TModel? Edit(int id) => _service.Edit(id);


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("hide/{id}")]
    public virtual TModel? Hide(int id) => _service.Hide(id);


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost("insert")]
    public virtual IActionResult Insert([FromBody] TInsert insertRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _service.Insert(insertRequest);
        return Ok(result);
    }


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("update/{id}")]
    public virtual IActionResult Update(int id, [FromBody] TUpdate updateRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = _service.Update(id, updateRequest);
        return Ok(result);
    }
}
