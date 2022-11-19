using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services.Models;

public class RoleService : IModelService<Role>
{
    public static List<Role> GetAll()
    {
        List<Role> resultList = DatabaseService.Execute<Role>("SELECT * FROM [dbo].[role]");
        
        return resultList;
    }

    public static Role FindByID(int id)
    {
        Role result = DatabaseService.ExecuteSingle<Role>("SELECT * FROM [dbo].[role] WHERE id = " + id);
        
        return result;
    }

    public static bool Delete(Role obj)
    {
        int roleId = obj.Id;
        
        DatabaseService.Execute("DELETE FROM [dbo].[role] WHERE id = " + roleId);
        
        return true;
    }

    public static bool Update(Role obj)
    {
        int roleId = obj.Id;
        string q = "title = '" + obj.Title + "' WHERE id = " + roleId;
        
        DatabaseService.Execute("UPDATE [dbo].[role] SET " + q);
        
        return true;
    }

    public static bool Create(Role obj)
    {
        string title = obj.Title;
        
        DatabaseService.Execute("INSERT INTO [dbo].[role] (title) VALUES ('" + title + "')");
        
        return true;
    }
}