

using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers
{
    public class UserController
    {
        private UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        public User Authenticate(string email, string password)
        {
            return _userService.Authenticate(email, UtilityService.HashPassword(password));
        }
    }
}
