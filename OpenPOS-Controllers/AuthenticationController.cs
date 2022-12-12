using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers
{
    public class AuthenticationController
    {
        private RoleService _roleService;
        private UserService _userService;

        public AuthenticationController()
        {
            _roleService = new RoleService();
            _userService = new UserService();
        }
        public Role GetUserRole(string Username, string Password)
        {
            return _roleService.FindUserRole(Authenticate(Username, Password).Id);
        }

        public User Authenticate(string Username, string Password)
        {
            return _userService.Authenticate(Username, Password);
        }
    }
}
