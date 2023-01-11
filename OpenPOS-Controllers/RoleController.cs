using OpenPOS_Database.ModelServices;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Controllers;

public class RoleController
{
    private RoleService _roleService;

    public RoleController()
    {
        _roleService = new RoleService();
    }
    
    /// <summary>
    /// Gets all Roles
    /// </summary>
    /// <returns>List of all Roles</returns>
    public List<Role> GetRoles()
    {
        return _roleService.GetAll();
    }
    
    /// <summary>
    /// Gets a Role by Id
    /// </summary>
    /// <param name="id">RoleId</param>
    /// <returns>Role model</returns>
    public Role GetRole(int id)
    {
        return _roleService.FindByID(id);
    }

    /// <summary>
    /// Finds a Role by RoleName
    /// </summary>
    /// <param name="role">Role enum</param>
    /// <returns>Role model</returns>
    public Role FindOnTitle(RolesEnum role)
    {
        return _roleService.FindOnRoleTitle(role);
    }
    
    
}