using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices;

public class BillService : IModelService<Bill>
{
    /// <summary>
    /// Returns all Bills from database
    /// </summary>
    /// <returns>All Bills in list of models</returns>
    public List<Bill> GetAll()
    {
        List<Bill> resultList = DatabaseService.Execute<Bill>(new SqlCommand("SELECT * FROM [dbo].[Bill]"));

        return resultList;
    }

    /// <summary>
    /// Returns a Bill by id
    /// </summary>
    /// <param name="id">BillId</param>
    /// <returns>Bill</returns>
    public Bill FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Bill] WHERE [id] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Bill result = DatabaseService.ExecuteSingle<Bill>(query);

        return result;
    }

    /// <summary>
    /// Deletes the Bill given by id
    /// </summary>
    /// <param name="obj">Bill model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Bill obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Bill] WHERE [id] = @BillId");

        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = obj.Id;

        return DatabaseService.Execute(query);

    }

    /// <summary>
    /// Updates the Bill by given id and other data
    /// </summary>
    /// <param name="obj">Bill model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Bill obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Bill] SET [user_id] = @userid, [paid] = @paid, [updated_at] = @updated_at WHERE [id] = @id");

        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@paid", SqlDbType.Bit);
        query.Parameters["@paid"].Value = obj.Paid;
        query.Parameters.Add("@id", SqlDbType.Int);
        query.Parameters["@id"].Value = obj.Id;
        query.Parameters.Add("@updated_at", SqlDbType.DateTime);
        query.Parameters["@updated_at"].Value = DateTime.Now;

        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new Bill with given data
    /// </summary>
    /// <param name="obj">Bill model</param>
    /// <returns>Updated Bill model</returns>
    public Bill Create(Bill obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Bill] ([user_id], [paid], [created_at], [updated_at])  OUTPUT  inserted.*  VALUES (@userid, @paid, @created_at, @updated_at)");

        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@paid", SqlDbType.Bit);
        query.Parameters["@paid"].Value = obj.Paid;
        query.Parameters.Add("@created_at", SqlDbType.DateTime);
        query.Parameters["@created_at"].Value = obj.Created_at;
        query.Parameters.Add("@updated_at", SqlDbType.DateTime);
        query.Parameters["@updated_at"].Value = obj.Updated_at;

        return DatabaseService.ExecuteSingle<Bill>(query);
    }
}