using EasyPC.Model.Requests.UserRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        protected IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("get")]
        public List<Model.User>? Get([FromQuery]UserSearchObject? userSearch)
        {
            return _service.Get(userSearch);
        }
        [HttpGet("get/{id}")]
        public Model.User? GetUserById(int id)
        {
            return _service.GetUserById(id);
        }

        [HttpPost("login")]
        public Model.User? Login([FromBody] LoginRequest request)
        {
            return _service.Login(request.Username, request.Password);
        }

        [HttpPost("register")]
        public Model.User? Register([FromBody] RegisterRequest request)
        {
            return _service.Register(request.Username, request.Email, request.Password);
        }

        [HttpPost("update")]
        public Model.User? Update(int id,UserUpdateRequest request)
        {
            return _service.Update(id,request);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("update-role")]
        public Model.User? UpdateRole([FromBody] UpdateRoleRequest request)
        {
            return _service.UpdateRole(request);
        }

        [HttpPut("delete/{id}")]
        public Model.User? Delete(int id)
        {
            return _service.Delete(id);
        }

        [HttpPut("restore/{id}")]
        public Model.User? Restore(int id)
        {
            return _service.Restore(id);
        }
    }
}
