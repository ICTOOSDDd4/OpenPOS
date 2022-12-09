using OpenPOS_APP.Services.Interfaces;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers.Services
{
    public class AccessLevelService : IAccessLevel
    {
        public static bool IsAuthorized(User user, string role)
        {
            RoleService roleService = new();
            var result = roleService.FindUserRole(user.Id).Title.Equals(role);

            return result;
        }
    }
}
