using OpenPOS_Controllers.Services;
using OpenPOS_Database.ModelServices;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers
{
    public class UserController
    {
        private UserService _userService;
        private UserRoleService _userRoleService;
        private UtilityService _utilityService;

        public UserController()
        {
            _userService = new UserService();
            _userRoleService = new UserRoleService();
        }
        
        public User CreateNew(string firstName, string lastName, string email, string password)
        {
            User newUser = new User
            {
                Name = firstName,
                Last_name = lastName,
                Email = email,
                Password = password
            };
            return _userService.Create(newUser);
        }
        
        public UserRole AddRoleToUser(int userId, int roleId)
        {
            UserRole newUserRole = new UserRole
            {
                User_id = userId,
                Role_id = roleId
            };
            return _userRoleService.Create(newUserRole);
        }

        public User FindByEmail(string email)
        {
           return _userService.FindByEmail(email);
        }

    }
}
