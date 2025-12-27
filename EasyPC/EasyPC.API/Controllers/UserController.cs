using EasyPC.Model;
using EasyPC.Model.Requests.UserRequests;
using EasyPC.Model.SearchObjects;
using EasyPC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyPC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    protected IUserService _service = service;


    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpGet("get")]
    public PagedResult<User>? Get([FromQuery] UserSearchObject? userSearch) => _service.Get(userSearch);

    [Authorize]
    [HttpGet("get/{id}")]
    public User? GetUserById(int id) => _service.GetUserById(id);

    [AllowAnonymous]
    [HttpPost("register")]
    public User? Register([FromBody] RegisterRequest request) => _service.Register(request.Username, request.Email, request.Password);

    [Authorize]
    [HttpPost("update")]
    public User? Update(int id, UserUpdateRequest request) => _service.Update(id, request);

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("update-role")]
    public User? UpdateRole([FromBody] UpdateRoleRequest request) => _service.UpdateRole(request);

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("delete/{id}")]
    public User? Delete(int id) => _service.Delete(id);

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPut("restore/{id}")]
    public User? Restore(int id) => _service.Restore(id);

    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<User> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required" });
        }

        var user = _service.Login(request.Username, request.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(user);
    }
}
