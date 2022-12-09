using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OpenPOS_Database.Services.Models;

public class CategoryService : IModelService<Category>
{
    public static List<Category> GetAll()
    {
        List<Category> resultList = DatabaseService.Execute<Category>(new SqlCommand("SELECT * FROM [dbo].[Category]"));
        return resultList;
    }

    public static Category FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Category result = DatabaseService.ExecuteSingle<Category>(query);

        return result;
    }

    public static bool Delete(Category obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Category] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(Category obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Category] SET [Name] = @Name WHERE [Id] = @ID");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static Category Create(Category obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Category] ([Name])  OUTPUT  inserted.*  VALUES (@Name)");
        
        query.Parameters.Add("@Name", SqlDbType.VarChar);
        query.Parameters["@Name"].Value = obj.Name;
        
        return DatabaseService.ExecuteSingle<Category>(query);
    }
}