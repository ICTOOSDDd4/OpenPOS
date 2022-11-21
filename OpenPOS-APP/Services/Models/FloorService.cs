using System.Data;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class FloorService : IModelService<Floor>
{
    public static List<Floor> GetAll()
    {
        List<Floor> resultList = DatabaseService.Execute<Floor>(new SqlCommand("SELECT * FROM [dbo].[Floor]"));

        return resultList;
    }

    public static Floor FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Floor result = DatabaseService.ExecuteSingle<Floor>(query);

        return result;
    }

    public static bool Delete(Floor obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(Floor obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Floor] SET [storey] = @Storey WHERE [Id] = @ID");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static Floor Create(Floor obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Floor] ([storey])  OUTPUT  inserted.*  VALUES (@Storey)");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        
        return DatabaseService.ExecuteSingle<Floor>(query);
    }
}