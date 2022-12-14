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
        public Role GetUserRole(string Username, string Password)
        {
            return _roleService.FindUserRole(Authenticate(Username, Password).Id);
        }

        public User Authenticate(string Username, string Password)
        {
            return _userService.Authenticate(Username, _utilityService.HashPassword(Password));
        }

        public bool IsAuthorized(User user, RolesEnum role)
        {
            return _accessLevelService.IsAuthorized(user, role);
        }
    }
}
