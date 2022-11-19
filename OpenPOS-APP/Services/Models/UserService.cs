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
        int userId = obj.Id;
        
        DatabaseService.Execute("DELETE FROM [dbo].[user] WHERE id = " + userId);
        
        return true;
    }

    public static bool Update(User obj)
    {
        int userId = obj.Id;
        string q = "name = '" + obj.Name + "', last_name = '" + obj.Last_name + "', password = '" + obj.Password +
                   "', email = '" + obj.Email + "' WHERE id = " + userId;
        
        DatabaseService.Execute("UPDATE [dbo].[user] SET " + q);
        
        return true;
    }

    public static bool Create(User obj)
    {
        string q = "'" + obj.Name + "', '" + obj.Last_name + "', '" + obj.Password + "', '" + obj.Email + "'";
        
        DatabaseService.Execute("INSERT INTO [dbo].[user] (name, last_name, password, email) VALUES (" + q + ")");
        
        return true;
    }
}