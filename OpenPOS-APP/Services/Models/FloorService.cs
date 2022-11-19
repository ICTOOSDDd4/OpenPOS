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
        int floorInt = obj.Id;
        
        DatabaseService.Execute(new SqlCommand("DELETE FROM [dbo].[Floor] WHERE [ID] = " + floorInt));
        
        return true;
    }

    public static bool Update(Floor obj)
    {
        int floorInt = obj.Id;
        string q = "storey = '" + obj.Storey + "' WHERE [ID] = " + floorInt;
        
        DatabaseService.Execute(new SqlCommand("UPDATE [dbo].[Floor] SET " + q));
        
        return true;
    }

    public static bool Create(Floor obj)
    {
        string q = "'" + obj.Storey + "'";
        
        DatabaseService.Execute(new SqlCommand("INSERT INTO [dbo].[Floor] ([storey]) VALUES (" + q + ")"));
        
        return true;
    }
}