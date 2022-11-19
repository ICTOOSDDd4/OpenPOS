using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class ProductService : IModelService<Product>
{
    public static List<Product> GetAll()
    {
        List<Product> resultList = DatabaseService.Execute<Product>(new SqlCommand("SELECT * FROM [dbo].[Product]"));

        return resultList;
    }

    public static Product FindByID(int id)
    {
        Product result = DatabaseService.ExecuteSingle<Product>(new SqlCommand("SELECT * FROM [dbo].[Product] WHERE [Id] = " + id));
        
        return result;
    }

    public static bool Delete(Product obj)
    {
        int productId = obj.Id;
        
        DatabaseService.Execute(new SqlCommand("DELETE FROM [dbo].[Product] WHERE [Id] = " + productId));
        
        return true;
    }

    public static bool Update(Product obj)
    {
        int productId = obj.Id;
        string q = "[Name] = '" + obj.Name + "', [Price] = " + obj.Price + ", [Description] = '" + obj.Description + "' WHERE [Id] = " + productId;
        
        DatabaseService.Execute(new SqlCommand("UPDATE [dbo].[Product] SET " + q));

        return true;
    }

    public static bool Create(Product obj)
    {
        string q = "('" + obj.Name + "', " + obj.Price + ", '" + obj.Description + "')";
        
        DatabaseService.Execute(new SqlCommand("INSERT INTO [dbo].[Product] ([Name], [Price], [Description]) VALUES " + q));
        
        return true;
    }
}