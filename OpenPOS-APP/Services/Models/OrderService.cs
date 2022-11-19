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
        
        query.Parameters.Add("@@OrderId", SqlDbType.Int);
        query.Parameters["@@OrderId"].Value = obj.Id;
        
        return DatabaseService.Execute(query);
    }

    public static bool Update(Order obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[order] SET [status] = @status, [user_id] = @userid, [bill_id] = @billId = @id");

        query.Parameters.Add("@status", SqlDbType.TinyInt);
        query.Parameters["@status"].Value = obj.Status;
        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@billId", SqlDbType.Int);
        query.Parameters["@billId"].Value = obj.Bill_id;

        return DatabaseService.Execute(query);
    }

    public static bool Create(Order obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[order] ([status], [user_id], [bill_id]) VALUES (@status, @userid, @billId)");

        query.Parameters.Add("@status", SqlDbType.TinyInt);
        query.Parameters["@status"].Value = obj.Status;
        query.Parameters.Add("@userid", SqlDbType.Int);
        query.Parameters["@userid"].Value = obj.User_id;
        query.Parameters.Add("@billId", SqlDbType.Int);
        query.Parameters["@billId"].Value = obj.Bill_id;

        return DatabaseService.Execute(query);
    }
}