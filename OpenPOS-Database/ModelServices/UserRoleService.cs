using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices;

public class UserRoleService: IModelService<UserRole>
{
    /// <summary>
    /// Returns all UserRoles in the database
    /// </summary>
    /// <returns>A list of all UserRoles</returns>
    public List<UserRole> GetAll()
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[User_role]");

        return DatabaseService.Execute<UserRole>(query);
    }

    /// <summary>
    /// Returns a UserRole by UserId
    /// </summary>
    /// <param name="user_id">UserId</param>
    /// <returns>UserRole</returns>
    public UserRole FindByID(int user_id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[User_role] WHERE [user_id] = @id");
        query.Parameters.AddWithValue("@id", user_id);

        return DatabaseService.ExecuteSingle<UserRole>(query);
    }

    /// <summary>
    /// Deletes the UserRole given by ids
    /// </summary>
    /// <param name="obj">User model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(UserRole obj)
    {
       SqlCommand query = new SqlCommand("DELETE FROM [dbo].[User_role] WHERE [user_id] = @id");
       query.Parameters.AddWithValue("@id", obj.User_id);
       
         return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the UserRole by given id and other data
    /// </summary>
    /// <param name="obj">UserRole model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(UserRole obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[User_role] SET [role_id] = @role_id WHERE [user_id] = @id");
        query.Parameters.AddWithValue("@id", obj.User_id);
        query.Parameters.AddWithValue("@role_id", obj.Role_id);

        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new UserRole with given data
    /// </summary>
    /// <param name="obj">UserRole model</param>
    /// <returns>Updated UserRole model</returns>
    public UserRole Create(UserRole obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[User_role] ([user_id], [role_id]) VALUES (@user_id, @role_id)");
        query.Parameters.AddWithValue("@user_id", obj.User_id);
        query.Parameters.AddWithValue("@role_id", obj.Role_id);
        
        return DatabaseService.ExecuteSingle<UserRole>(query);
    }
}