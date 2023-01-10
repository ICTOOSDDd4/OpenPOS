using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class ProductService : IModelService<Product>
{
    /// <summary>
    /// Returns all Products from database
    /// </summary>
    /// <returns>All Products in list of models</returns>
    public List<Product> GetAll()
    {
        List<Product> resultList = DatabaseService.Execute<Product>(new SqlCommand("SELECT * FROM [dbo].[Product]"));

        return resultList;
    }
    
   public List<Product> GetAllByFilter(string filter)
   {
      string searchTerm = string.Format("%{0}%", filter);
      SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[product] WHERE [name] LIKE @Filter");

      query.Parameters.Add("@Filter", SqlDbType.VarChar);
      query.Parameters["@Filter"].Value = searchTerm;

      Debug.WriteLine(query.ToString());

      var result = DatabaseService.Execute<Product>(query);
        foreach (Product prod in result)
        {
            Debug.WriteLine(prod.Name);
        }
      return result;
    }
    
    public List<Product> GetAllByCategoryId(int categoryId)
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

    /// <summary>
    /// Returns a Product by id
    /// </summary>
    /// <param name="id">ProductId</param>
    /// <returns>Product</returns>
    public Product FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Product] WHERE [ID] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Product result = DatabaseService.ExecuteSingle<Product>(query);
        
        return result;
    }

    /// <summary>
    /// Deletes the Product given by id
    /// </summary>
    /// <param name="obj">Product model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Product obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[product] WHERE [ID] = @ProductID");
        
        query.Parameters.Add("@ProductID", SqlDbType.Int);
        query.Parameters["@ProductID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the Product by given id and other data
    /// </summary>
    /// <param name="obj">Product model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Product obj)
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

    /// <summary>
    /// Creates a new Product with given data
    /// </summary>
    /// <param name="obj">Product model</param>
    /// <returns>Updated Product model</returns>
    public Product Create(Product obj)
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