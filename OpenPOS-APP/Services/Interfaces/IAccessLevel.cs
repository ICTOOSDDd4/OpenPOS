using OpenPOS_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Services.Interfaces
{
    public interface IAccessLevel
    {
        public static abstract bool IsAuthorized(User user, string role);
    }
    
}
