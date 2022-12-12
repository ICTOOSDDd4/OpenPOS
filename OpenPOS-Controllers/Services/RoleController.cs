using OpenPOS_Database.Services.Models;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Controllers.Services;

public class RoleController
{
    private RoleService _roleService;

    public RoleController()
    {
        _roleService = new RoleService();
    }
    
    public List<Role> GetRoles()
    {
        return _roleService.GetAll();
    }
    
    public Role GetRole(int id)
    {
        return _roleService.FindByID(id);
    }

    public Role FindOnTitle(RolesEnum role)
    {
        return _roleService.FindOnRoleTitle(role);
    }
    
    
}