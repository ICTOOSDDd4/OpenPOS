using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class UserService : IModelService<User>
{
    /// <summary>
    /// Returns all Users from database
    /// </summary>
    /// <returns>All Users in list of models</returns>
    public List<User> GetAll()
    {
        List<User> resultList = DatabaseService.Execute<User>(new SqlCommand("SELECT * FROM [dbo].[user]"));

        return resultList;
    }

    /// <summary>
    /// Returns a User by id
    /// </summary>
    /// <param name="id">UserId</param>
    /// <returns>User</returns>
    public User FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[user] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        User result = DatabaseService.ExecuteSingle<User>(query);

        return result;
    }

    /// <summary>
    /// Deletes the User given by id
    /// </summary>
    /// <param name="obj">User model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(User obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[user] WHERE [id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the User by given id and other data
    /// </summary>
    /// <param name="obj">User model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(User obj)
    {
        Console.WriteLine(obj.Password);
        SqlCommand query = new SqlCommand("UPDATE [dbo].[user] SET [name] = @Name, [last_name] = @LastName, [password] = @Password WHERE [id] = @ID");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@LastName", SqlDbType.VarChar);
        query.Parameters["@LastName"].Value = obj.Last_name;
        query.Parameters.Add("@Password", SqlDbType.VarChar);
        query.Parameters["@Password"].Value = obj.Password;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new User with given data
    /// </summary>
    /// <param name="obj">User model</param>
    /// <returns>Updated User model</returns>
    public User Create(User obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[user] ([name], [last_name], [email], [password]) OUTPUT inserted.* VALUES (@Name, @LastName, @Email, @Password)");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@LastName", SqlDbType.VarChar);
        query.Parameters["@LastName"].Value = obj.Last_name;
        query.Parameters.Add("@Email", SqlDbType.VarChar);
        query.Parameters["@Email"].Value = obj.Email;
        query.Parameters.Add("@Password", SqlDbType.VarChar);
        query.Parameters["@Password"].Value = obj.Password;

        return DatabaseService.ExecuteSingle<User>(query);
    }

    /// <summary>
    /// Authenticates a user attempting to loggin
    /// </summary>
    /// <param name="email">Email adres</param>
    /// <param name="password">Entered password</param>
    /// <returns>User if succesful login. Null of not succesful</returns>
    public User Authenticate(string email, string password)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[user] WHERE [email] = @Email AND [password] = @Password");
        
        query.Parameters.Add("@Email", SqlDbType.VarChar);
        query.Parameters["@Email"].Value = email;
        query.Parameters.Add("@Password", SqlDbType.VarChar);
        query.Parameters["@Password"].Value = password;
        
        User user = DatabaseService.ExecuteSingle<User>(query);
        
        if (user.Email == null)
        {
            return null;
        }
        return user;
    }

    /// <summary>
    /// Finds a user by given email
    /// </summary>
    /// <param name="email">User email</param>
    /// <returns>User if a user with that email exists. Null of not found</returns>
    public User FindByEmail(string email)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[user] WHERE [email] = @Email");
        
        query.Parameters.Add("@Email", SqlDbType.VarChar);
        query.Parameters["@Email"].Value = email;
        
        User user = DatabaseService.ExecuteSingle<User>(query);
        
        if (user.Email == null)
        {
            return null;
        }
        return user;
    }
}