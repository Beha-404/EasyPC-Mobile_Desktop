using EasyPC.Model;
using EasyPC.Model.Requests.UserRequests;
using EasyPC.Model.SearchObjects;

namespace EasyPC.Services.Interfaces;

public interface IUserService
{
    public User? Login(string username, string password);
    public User? Register(string username, string email, string password);
    public User? Delete(int id);
    public User? Restore(int id);
    public User? Update(int id, UserUpdateRequest updateRequest);
    public User? UpdateRole(UpdateRoleRequest updateRoleRequest);
    public PagedResult<User>? Get(UserSearchObject? userSearchObject);
    public User? GetUserById(int id);
}
