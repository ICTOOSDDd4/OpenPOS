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
            _utilityService = new UtilityService();
        }
        
        /// <summary>
        /// Creates new User using the UserService
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="email">Email adres</param>
        /// <param name="password">Hashed password</param>
        /// <returns>The newly created User</returns>
        public User CreateNew(string firstName, string lastName, string email, string password)
        {
            User newUser = new User
            {
                Name = firstName,
                Last_name = lastName,
                Email = email,
                Password = _utilityService.HashPassword(password)
            };
            return _userService.Create(newUser);
        }
        
        /// <summary>
        /// Assigns a Role to a User
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="roleId">RoleId</param>
        /// <returns>The newly created UserRole</returns>
        public UserRole AddRoleToUser(int userId, int roleId)
        {
            UserRole newUserRole = new UserRole
            {
                User_id = userId,
                Role_id = roleId
            };
            return _userRoleService.Create(newUserRole);
        }

        /// <summary>
        /// Finds a User by email adres
        /// </summary>
        /// <param name="email">Email adres</param>
        /// <returns>The found User</returns>
        public User FindByEmail(string email)
        {
           return _userService.FindByEmail(email);
        }

    }
}
