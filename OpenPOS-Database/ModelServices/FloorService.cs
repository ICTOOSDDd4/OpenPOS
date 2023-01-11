using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices;

public class FloorService : IModelService<Floor>
{
    /// <summary>
    /// Returns all Floors from database
    /// </summary>
    /// <returns>All Floors in list of models</returns>
    public List<Floor> GetAll()
    {
        List<Floor> resultList = DatabaseService.Execute<Floor>(new SqlCommand("SELECT * FROM [dbo].[Floor]"));

        return resultList;
    }

    /// <summary>
    /// Returns a Floor by id
    /// </summary>
    /// <param name="id">FloorId</param>
    /// <returns>Floor</returns>
    public Floor FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;
        
        Floor result = DatabaseService.ExecuteSingle<Floor>(query);

        return result;
    }

    /// <summary>
    /// Deletes the Floor given by id
    /// </summary>
    /// <param name="obj">Floor model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Floor obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Floor] WHERE [Id] = @ID");
        
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the Floor by given id and other data
    /// </summary>
    /// <param name="obj">Floor model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Floor obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Floor] SET [storey] = @Storey WHERE [Id] = @ID");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new Floor with given data
    /// </summary>
    /// <param name="obj">Floor model</param>
    /// <returns>Updated Floor model</returns>
    public Floor Create(Floor obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Floor] ([storey])  OUTPUT  inserted.*  VALUES (@Storey)");
        
        query.Parameters.Add("@Storey", SqlDbType.VarChar);
        query.Parameters["@Storey"].Value = obj.Storey;
        
        return DatabaseService.ExecuteSingle<Floor>(query);
    }
}