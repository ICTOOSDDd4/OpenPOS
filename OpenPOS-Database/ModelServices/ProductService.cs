using System.Data;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;
using System.Diagnostics;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class ProductService : IModelService<Product>
{
    public static List<Product> GetAll()
    {
        List<Product> resultList = DatabaseService.Execute<Product>(new SqlCommand("SELECT * FROM [dbo].[Product]"));

        return resultList;
    }

   public static List<Product> GetAllByFilter(string filter)
   {
      string searchTerm = string.Format("%{0}%", filter);
      SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[product] WHERE [name] LIKE @Filter");

      query.Parameters.Add("@Filter", SqlDbType.VarChar);
      query.Parameters["@Filter"].Value = searchTerm;

      Debug.WriteLine(query.ToString());

      var result = DatabaseService.Execute<Product>(query);
        foreach (Product prod in result)
        {
            System.Diagnostics.Debug.WriteLine(prod.Name);
        }
      return result;
    }
    
    public static List<Product> GetAllByCategoryId(int categoryId)
    {
        List<Product> result;
        if (categoryId != 0)
        {
            SqlCommand query = new SqlCommand("SELECT p.* FROM [dbo].[Category_product] AS C JOIN [dbo].[product] AS p ON C.productid = p.id WHERE C.Categoryid = @ID");

            query.Parameters.Add("@ID", SqlDbType.Int);
            query.Parameters["@ID"].Value = categoryId;
            result = DatabaseService.Execute<Product>(query);
        } else
        {
            result = GetAll();
        }
        return result;
    }

    public static Product FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Product result = DatabaseService.ExecuteSingle<Product>(query);
        
        return result;
    }

    public static bool Delete(Product obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[product] WHERE [ID] = @ProductID");
        
        query.Parameters.Add("@ProductID", SqlDbType.Int);
        query.Parameters["@ProductID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(Product obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Product] SET [Name] = @Name, [Price] = @Price, [description] = @Description WHERE [ID] = @ID");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@Price", SqlDbType.Decimal);
        query.Parameters["@Price"].Value = obj.Price;
        query.Parameters.Add("@Description", SqlDbType.VarChar);
        query.Parameters["@Description"].Value = obj.Description;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static Product Create(Product obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Product] ([Name], [Price], [Description])  OUTPUT  inserted.*  VALUES (@Name, @Price, @Description)");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@Price", SqlDbType.Decimal);
        query.Parameters["@Price"].Value = obj.Price;
        query.Parameters.Add("@Description", SqlDbType.VarChar);
        query.Parameters["@Description"].Value = obj.Description;
        
        return DatabaseService.ExecuteSingle<Product>(query);
    }
}