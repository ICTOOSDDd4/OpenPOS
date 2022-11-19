using System.Data;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

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

    public static bool Create(Role obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Role] ([title]) VALUES (@Title)");
        
        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = obj.Title;
        
        return DatabaseService.Execute(query);
    }
}