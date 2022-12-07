using System.Data;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class OrderService : IModelService<Order>
{
    public static List<Order> GetAll()
    {
        List<Order> resultList = DatabaseService.Execute<Order>(new SqlCommand("SELECT * FROM [dbo].[Order]"));

        return resultList;
    }

    public static List<Order> GetAllOpenOrders()
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE [status] = @status");
        query.Parameters.Add("@status", SqlDbType.TinyInt);
        query.Parameters["@status"].Value = false;

        return DatabaseService.Execute<Order>(query);
    }

    public static Order FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE [Id] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Order result = DatabaseService.ExecuteSingle<Order>(query);

        return result;
    }

    public static bool Delete(Order obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[order] WHERE [ID] = @OrderId");

        query.Parameters.Add("@OrderId", SqlDbType.Int);
        query.Parameters["@OrderId"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    public static bool Update(Order obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[order] SET [status] = @status, [user_id] = @userid, [bill_id] = @billId WHERE [id] = @id");

        query.Parameters.Add("@status", SqlDbType.TinyInt);
        query.Parameters["@status"].Value = obj.Status;
        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@billId", SqlDbType.Int);
        query.Parameters["@billId"].Value = obj.Bill_id;
        query.Parameters.Add("@id", SqlDbType.Int);
        query.Parameters["@id"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    public static Order Create(Order obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[order] ([status], [user_id], [bill_id], [created_at], [updated_at])  OUTPUT  inserted.*  VALUES (@status, @userid, @billId, @created_at, @updated_at)");

        query.Parameters.Add("@status", SqlDbType.TinyInt);
        query.Parameters["@status"].Value = obj.Status;
        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@billId", SqlDbType.Int);
        query.Parameters["@billId"].Value = obj.Bill_id;
        query.Parameters.Add("@created_at", SqlDbType.DateTime);
        query.Parameters["@created_at"].Value = obj.Created_At;
        query.Parameters.Add("@updated_at", SqlDbType.DateTime);
        query.Parameters["@updated_at"].Value = obj.Updated_At;

        var result = DatabaseService.ExecuteSingle<Order>(query);

        //foreach (var line in result.Lines)
        //{
        //   OrderLineService.Create(line);
        //}

        return result;
    }
}