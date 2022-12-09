using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class FloorService : IModelService<Floor>
{
    public List<Floor> GetAll()
    {
        List<Floor> resultList = DatabaseService.Execute<Floor>(new SqlCommand("SELECT * FROM [dbo].[Floor]"));

        return resultList;
    }

    public Floor FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Floor result = DatabaseService.ExecuteSingle<Floor>(query);

        return result;
    }

    public bool Delete(Floor obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public bool Update(Floor obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Floor] SET [storey] = @Storey WHERE [Id] = @ID");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public Floor Create(Floor obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Floor] ([storey])  OUTPUT  inserted.*  VALUES (@Storey)");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        
        return DatabaseService.ExecuteSingle<Floor>(query);
    }
}