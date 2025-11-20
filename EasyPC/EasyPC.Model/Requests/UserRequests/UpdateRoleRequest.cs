using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPC.Model.Requests.UserRequests
{
    public class UpdateRoleRequest
    {
        public int UserId { get; set; }
        public UserRole NewRole { get; set; }
    }
}
