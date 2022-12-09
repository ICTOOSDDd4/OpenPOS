

using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

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
