using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using OpenPOS_APP.Services.Models;

namespace OpenPOS_APP.Services
{
    public class AccessLevelService : IAccessLevel
    {
        public static bool IsAuthorized(User user, string role)
        {
            var result = RoleService.FindUserRole(user.Id).Title.Equals(role);

            return result;
        }
    }
}
