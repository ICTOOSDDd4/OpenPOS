using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices;

public class CategoryService : IModelService<Category>
{
    /// <summary>
    /// Returns all Category from database
    /// </summary>
    /// <returns>All Category in list of models</returns>
    public List<Category> GetAll()
    {
        List<Category> resultList = DatabaseService.Execute<Category>(new SqlCommand("SELECT * FROM [dbo].[Category]"));
        return resultList;
    }

    /// <summary>
    /// Returns a Category by id
    /// </summary>
    /// <param name="id">CategoryId</param>
    /// <returns>Category</returns>
    public Category FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Category result = DatabaseService.ExecuteSingle<Category>(query);

        return result;
    }

    /// <summary>
    /// Deletes the Category given by id
    /// </summary>
    /// <param name="obj">Category model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Category obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the Category by given id and other data
    /// </summary>
    /// <param name="obj">Category model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Category obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Category] SET [Name] = @Name WHERE [Id] = @ID");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new Category with given data
    /// </summary>
    /// <param name="obj">Category model</param>
    /// <returns>Updated Category model</returns>
    public Category Create(Category obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Category] ([Name])  OUTPUT  inserted.*  VALUES (@Name)");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        
        return DatabaseService.ExecuteSingle<Category>(query);
    }
}