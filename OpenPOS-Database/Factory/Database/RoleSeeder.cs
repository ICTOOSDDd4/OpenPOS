using OpenPOS_APP.Enums;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Database.Factory.Database
{
    public class RoleSeeder
    {
        private static IDictionary<RolesEnum, string> _roles = new Dictionary<RolesEnum, string>()
        {
            { RolesEnum.Owner, RolesEnum.Owner.ToString() },
            { RolesEnum.Admin, RolesEnum.Admin.ToString() },
            { RolesEnum.Cook, RolesEnum.Cook.ToString() },
            { RolesEnum.Crew, RolesEnum.Crew.ToString() },
            { RolesEnum.Bar, RolesEnum.Bar.ToString() },
            { RolesEnum.Guest, RolesEnum.Guest.ToString() }
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
