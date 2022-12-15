using OpenPOS_APP.Services.Interfaces;
using OpenPOS_Database.ModelServices;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Controllers.Services
{
    public class AccessLevelService : IAccessLevel
    {
        public bool IsAuthorized(User user, RolesEnum role)
        {
            RoleService roleService = new();

            var result = roleService.FindUserRole(user.Id).Title;

            return Enum.Parse<RolesEnum>(result).Equals(role);
        }
    }
}
