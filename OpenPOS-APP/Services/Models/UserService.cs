using System.Data;
using System.Data.SqlClient;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services.Models;

public class UserService : IModelService<User>
{
    public static List<User> GetAll()
    {
        List<User> resultList = DatabaseService.Execute<User>(new SqlCommand("SELECT * FROM [dbo].[user]"));

        return resultList;
    }

    public static User FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[user] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        User result = DatabaseService.ExecuteSingle<User>(query);

        return result;
    }

    public static bool Delete(User obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[user] WHERE [id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(User obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[user] SET [Name] = @Name, " +" [last_name] = @LastName, " + " [email] = @Email, " + " [password] = @Password " + " WHERE [id] = @ID", DatabaseService.Dbcontext);
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@LastName", SqlDbType.VarChar);
        query.Parameters["@LastName"].Value = obj.Last_name;
        query.Parameters.Add("@Email", SqlDbType.VarChar);
        query.Parameters["@Email"].Value = obj.Email;
        query.Parameters.Add("@Password", SqlDbType.VarChar);
        query.Parameters["@Password"].Value = obj.Password;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static User Create(User obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[user] ([Name], [last_name], [email], [password]) OUTPUT  inserted.* VALUES (@Name" + ", @LastName," + " @Email," + " @Password)", DatabaseService.Dbcontext);
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@LastName", SqlDbType.VarChar);
        query.Parameters["@LastName"].Value = obj.Last_name;
        query.Parameters.Add("@Email", SqlDbType.VarChar);
        query.Parameters["@Email"].Value = obj.Email;
        query.Parameters.Add("@Password", SqlDbType.VarChar);
        query.Parameters["@Password"].Value = obj.Password;
        
        Console.WriteLine(query.ToString());
        
        return DatabaseService.ExecuteSingle<User>(query);
    }
}