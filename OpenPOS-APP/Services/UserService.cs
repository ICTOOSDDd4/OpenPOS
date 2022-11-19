using System.Data;
using System.Data.SqlClient;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services;

public class UserService: IModelService<User>
{
    public static List<User> GetAll()
    {
        List<User> resultList = DatabaseService.Execute<User>("SELECT * FROM [dbo].[user]");

        return resultList;
    }

    public static User FindByID(int id)
    {
        throw new NotImplementedException();
    }

    public static bool Delete(User obj)
    {
        throw new NotImplementedException();
    }

    public static bool Update(User obj)
    {
        throw new NotImplementedException();
    }

    public static bool Create(User obj)
    {
        throw new NotImplementedException();
    }
}