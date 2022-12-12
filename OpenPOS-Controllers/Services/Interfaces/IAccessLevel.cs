using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_APP.Services.Interfaces
{
    public interface IAccessLevel
    {
        public bool IsAuthorized(User user, RolesEnum roles);
    }
    
}
