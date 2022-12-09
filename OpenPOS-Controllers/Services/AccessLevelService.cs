﻿using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using OpenPOS_Database.Services.Models;

namespace OpenPOS_Controllers.Services
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