using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;
using OpenPOS_Settings.Enums;

namespace OpenPOS_Database.ModelServices;

public class RoleService : IModelService<Role>
{
    /// <summary>
    /// Returns all Roles from database
    /// </summary>
    /// <returns>All Roles in list of models</returns>
    public List<Role> GetAll()
    {
        List<Role> resultList = DatabaseService.Execute<Role>(new SqlCommand("SELECT * FROM [dbo].[role]"));

        return resultList;
    }

    /// <summary>
    /// Returns a Role by id
    /// </summary>
    /// <param name="id">RoleId</param>
    /// <returns>Role</returns>
    public Role FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Role] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Role result = DatabaseService.ExecuteSingle<Role>(query);
        
        return result;
    }

    /// <summary>
    /// Deletes the Role given by id
    /// </summary>
    /// <param name="obj">Role model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Role obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Role] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the Role by given id and other data
    /// </summary>
    /// <param name="obj">Role model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Role obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Role] SET [title] = @Title WHERE [ID] = @ID");
        
        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = obj.Title;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new Role with given data
    /// </summary>
    /// <param name="obj">Role model</param>
    /// <returns>Updated Role model</returns>
    public Role Create(Role obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Role] ([title])  OUTPUT  inserted.*  VALUES (@Title)");
        
        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = obj.Title;
        
        return DatabaseService.ExecuteSingle<Role>(query);
    }

    public Role FindUserRole(int id)
    {
        SqlCommand query = new SqlCommand(
            "SELECT r.id, r.title FROM [OpenPOS_dev].[dbo].[user_role] as ur INNER JOIN [dbo].[user] as u ON ur.user_id = u.id INNER JOIN role as r ON ur.role_id = r.id WHERE u.id = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Role result = DatabaseService.ExecuteSingle<Role>(query);

        return result;
    }
    
    public Role FindOnRoleTitle(RolesEnum role)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Role] WHERE [title] = @Title");

        query.Parameters.Add("@Title", SqlDbType.VarChar);
        query.Parameters["@Title"].Value = role.ToString();

        Role result = DatabaseService.ExecuteSingle<Role>(query);

        return result;
    }
}