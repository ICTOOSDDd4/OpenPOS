using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OpenPOS_Database.Services.Models;

public class BillService : IModelService<Bill>
{
    public static List<Bill> GetAll()
    {
        List<Bill> resultList = DatabaseService.Execute<Bill>(new SqlCommand("SELECT * FROM [dbo].[Bill]"));

        return resultList;
    }

    public static Bill FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Bill] WHERE [id] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Bill result = DatabaseService.ExecuteSingle<Bill>(query);

        return result;
    }

    public static bool Delete(Bill obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Bill] WHERE [id] = @BillId");

        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = obj.Id;

        return DatabaseService.Execute(query);

    }

    public static bool Update(Bill obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[Bill] SET [user_id] = @userid, [paid] = @paid WHERE [id] = @id");

        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@paid", SqlDbType.Bit);
        query.Parameters["@paid"].Value = obj.Paid;
        query.Parameters.Add("@id", SqlDbType.Int);
        query.Parameters["@id"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    public static Bill Create(Bill obj)
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