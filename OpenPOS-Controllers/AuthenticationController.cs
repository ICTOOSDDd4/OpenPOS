using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_Controllers.Services;
using OpenPOS_Database.ModelServices;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Controllers
{
    public class AuthenticationController
    {
        private RoleService _roleService;
        private AccessLevelService _accessLevelService;
        private UserService _userService;
        private UtilityService _utilityService;

        public AuthenticationController()
        {
            _accessLevelService = new AccessLevelService();
            _roleService = new RoleService();
            _userService = new UserService();
            _utilityService = new UtilityService();
        }

        /// <summary>
        /// Gets a Users Role by Username and Password
        /// </summary>
        /// <param name="Username">Username of Logged in User</param>
        /// <param name="Password">Password of Logged in User</param>
        /// <returns></returns>
        public Role GetUserRole(string Username, string Password)
        {
            return _roleService.FindUserRole(Authenticate(Username, Password).Id);
        }

        /// <summary>
        /// Loggs in the User if credentials are correct
        /// </summary>
        /// <param name="Username">Filled in Username</param>
        /// <param name="Password">Filled in Password</param>
        /// <returns>Found User or Null if incorrect credentials</returns>
        public User Authenticate(string Username, string Password)
        {
            return _userService.Authenticate(Username, _utilityService.HashPassword(Password));
        }

        /// <summary>
        /// Checks if the logged in User has the Role given
        /// </summary>
        /// <param name="user">User that needs to be checked</param>
        /// <param name="role">Role that the User needs to be authorized</param>
        /// <returns>Bool if authorized or not</returns>
        public bool IsAuthorized(User user, RolesEnum role)
        {
            return _accessLevelService.IsAuthorized(user, role);
        }
    }
}
