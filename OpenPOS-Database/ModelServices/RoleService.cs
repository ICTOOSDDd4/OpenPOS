using System.Data;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;
using OpenPOS_APP.Enums;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class RoleService : IModelService<Role>
{
    public static List<Role> GetAll()
    {
        List<Role> resultList = DatabaseService.Execute<Role>(new SqlCommand("SELECT * FROM [dbo].[role]"));

        return resultList;
    }

    public static Role FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Role] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Role result = DatabaseService.ExecuteSingle<Role>(query);
        
        return result;
    }

    public static bool Delete(Role obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Role] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(Role obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Role] SET [title] = @Title WHERE [ID] = @ID");
        
        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = obj.Title;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static Role Create(Role obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Role] ([title])  OUTPUT  inserted.*  VALUES (@Title)");
        
        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = obj.Title;
        
        return DatabaseService.ExecuteSingle<Role>(query);
    }

    public static Role FindUserRole(int id)
    {
        SqlCommand query = new SqlCommand(
            "SELECT r.id, r.title FROM [OpenPOS_dev].[dbo].[user_role] as ur INNER JOIN [dbo].[user] as u ON ur.user_id = u.id INNER JOIN role as r ON ur.role_id = r.id WHERE u.id = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Role result = DatabaseService.ExecuteSingle<Role>(query);

        return result;
    }
    
    public static Role FindOnRoleTitle(RolesEnum role)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Role] WHERE [title] = @Title");

        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = role.ToString();

        Role result = DatabaseService.ExecuteSingle<Role>(query);

        return result;
    }
}