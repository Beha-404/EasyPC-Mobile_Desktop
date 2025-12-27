namespace EasyPC.Model.Requests.UserRequests;

public class UpdateRoleRequest
{
    public int UserId { get; set; }

    public UserRole NewRole { get; set; }
}
