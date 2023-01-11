using OpenPOS_APP.Services.Interfaces;
using OpenPOS_Database.ModelServices;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Controllers.Services
{
    public class AccessLevelService : IAccessLevel
    {
        /// <summary>
        /// Checks if a User Has a certain Role
        /// </summary>
        /// <param name="user">User to be checked</param>
        /// <param name="role">Role to be checked</param>
        /// <returns>Bool for if the User has the Role</returns>
        public bool IsAuthorized(User user, RolesEnum role)
        {
            RoleService roleService = new();

            var result = roleService.FindUserRole(user.Id).Title;

            return Enum.Parse<RolesEnum>(result).Equals(role);
        }
    }
}
