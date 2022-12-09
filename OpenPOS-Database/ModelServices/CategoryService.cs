using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class CategoryService : IModelService<Category>
{
    public List<Category> GetAll()
    {
        List<Category> resultList = DatabaseService.Execute<Category>(new SqlCommand("SELECT * FROM [dbo].[Category]"));
        return resultList;
    }

    public Category FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Category result = DatabaseService.ExecuteSingle<Category>(query);

        return result;
    }

    public bool Delete(Category obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public bool Update(Category obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Category] SET [Name] = @Name WHERE [Id] = @ID");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public Category Create(Category obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Category] ([Name])  OUTPUT  inserted.*  VALUES (@Name)");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        
        return DatabaseService.ExecuteSingle<Category>(query);
    }
}