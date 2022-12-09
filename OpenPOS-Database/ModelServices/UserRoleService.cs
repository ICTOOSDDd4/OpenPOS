using System.Data.SqlClient;
using OpenPOS_Database;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class UserRoleService: IModelService<UserRole>
{
    public List<UserRole> GetAll()
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[User_role]");

        return DatabaseService.Execute<UserRole>(query);
    }

    public UserRole FindByID(int user_id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[User_role] WHERE [user_id] = @id");
        query.Parameters.AddWithValue("@id", user_id);

        return DatabaseService.ExecuteSingle<UserRole>(query);
    }

    public bool Delete(UserRole obj)
    {
       SqlCommand query = new SqlCommand("DELETE FROM [dbo].[User_role] WHERE [user_id] = @id");
       query.Parameters.AddWithValue("@id", obj.User_id);
       
         return DatabaseService.Execute(query);
    }

    public bool Update(UserRole obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[User_role] SET [role_id] = @role_id WHERE [user_id] = @id");
        query.Parameters.AddWithValue("@id", obj.User_id);
        query.Parameters.AddWithValue("@role_id", obj.Role_id);

        return DatabaseService.Execute(query);
    }

    public UserRole Create(UserRole obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[User_role] ([user_id], [role_id]) VALUES (@user_id, @role_id)");
        query.Parameters.AddWithValue("@user_id", obj.User_id);
        query.Parameters.AddWithValue("@role_id", obj.Role_id);
        
        return DatabaseService.ExecuteSingle<UserRole>(query);
    }
}