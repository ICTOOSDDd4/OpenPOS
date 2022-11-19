using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class CategoryService : IModelService<Category>
{
    public static List<Category> GetAll()
    {
        List<Category> resultList = DatabaseService.Execute<Category>("SELECT * FROM [dbo].[Category]");
        return resultList;
    }

    public static Category FindByID(int id)
    {
        Category result = DatabaseService.ExecuteSingle<Category>("SELECT * FROM [dbo].[Category] WHERE [Id] = " + id);
        return result;
    }

    public static bool Delete(Category obj)
    {
        int categoryId = obj.Id;
        
       DatabaseService.Execute("DELETE FROM [dbo].[Category] WHERE [Id] = " + categoryId);
       
        return true;
    }

    public static bool Update(Category obj)
    {
        int categoryId = obj.Id;
        string q = "name = '" + obj.Name + "' WHERE [Id] = " + categoryId;
        
        DatabaseService.Execute("UPDATE [dbo].[Category] SET " + q);
        
        return true;
    }

    public static bool Create(Category obj)
    {
        DatabaseService.Execute("INSERT INTO [dbo].[Category] ([Name]) VALUES ('" + obj.Name + "')");
        
        return true;
    }
}