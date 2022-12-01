using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP.Factory.Database
{
    public class RoleSeeder
    {
        private static string[] _roles = {"Owner", "Admin", "Crew", "cook", "guest"};
        private static List<Role> _dbRoles;

        public static void Seed()
        {
            _dbRoles = RoleService.GetAll();
            foreach (var role in _roles)
            {
                if (_dbRoles.All(r => r.Title != role))
                    RoleService.Create(new Role(role));
            }
        }

    }
}
