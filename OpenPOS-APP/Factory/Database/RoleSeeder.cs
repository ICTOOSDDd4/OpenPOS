using OpenPOS_APP.Enums;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP.Factory.Database
{
    public class RoleSeeder
    {
        private static IDictionary<RolesEnum, string> _roles = new Dictionary<RolesEnum, string>()
        {
            { RolesEnum.Owner, "Owner" },
            { RolesEnum.Admin, "Admin" },
            { RolesEnum.Cook, "Cook" },
            { RolesEnum.Crew, "Crew" },
            { RolesEnum.Bar, "Bar" },
            { RolesEnum.Guest, "Guest" }
        };

        private static List<Role> _dbRoles;

        public static void Seed()
        {
            _dbRoles = RoleService.GetAll();
            foreach (RolesEnum role in Enum.GetValues(typeof(RolesEnum)))
            {
                if (_dbRoles.All(r => r.Title != _roles[role]))
                    RoleService.Create(new Role(_roles[role]));
            }
        }

    }
}
