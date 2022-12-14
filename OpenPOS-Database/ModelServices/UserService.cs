using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class UserService : IModelService<User>
{
    public List<User> GetAll()
    {
        List<User> resultList = DatabaseService.Execute<User>(new SqlCommand("SELECT * FROM [dbo].[user]"));

        return resultList;
    }

    public User FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[user] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        User result = DatabaseService.ExecuteSingle<User>(query);

        return result;
    }

    public bool Delete(User obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[user] WHERE [id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

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