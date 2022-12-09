using OpenPOS_APP.Models;
using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Controllers
{
    public static class UserController
    {
        private static UserService _userService = new();
        public static User Authenticate(string email, string password)
        {
            return _userService.Authenticate(email, UtilityService.HashPassword(password));
        }
    }
}
