using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EasyPC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TModel,TSearch, TInsert,TUpdate> : ControllerBase
    {
        protected IBaseService<TModel,TSearch, TInsert,TUpdate> _service;
        public BaseController(IBaseService<TModel, TSearch, TInsert,TUpdate> service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public virtual PagedResult<TModel> GetAll([FromQuery] TSearch search)
        {
            return _service.GetAll(search);
        }
        
        [HttpGet("get/{id}")]
        public virtual TModel? GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpGet("allowedActions/{id}")]
        public virtual List<string> AllowedActions(int id)
        {
            return _service.AllowedActions(id);
        }

        [HttpPost("insert")]
        public virtual TModel? Insert([FromBody]TInsert insertRequest)
        {
            return _service.Insert(insertRequest);
        }

        [HttpPut("update/{id}")]
        public virtual TModel? Update(int id, [FromBody]TUpdate updateRequest)
        {
            return _service.Update(id, updateRequest);
        }

        [HttpPut("activate/{id}")]
        public virtual TModel? Activate(int id)
        {
            return _service.Activate(id);
        }

        [HttpPut("edit/{id}")]
        public virtual TModel? Edit(int id)
        {
            return _service.Edit(id);
        }

        [HttpPut("hide/{id}")]
        public virtual TModel? Hide(int id)
        {
            return _service.Hide(id);
        }
    }
}
