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
        Order result = DatabaseService.ExecuteSingle<Order>("SELECT * FROM [dbo].[Order] WHERE ID = " + id);

        return result;
    }

    public static bool Delete(Order obj)
    {
        int orderId = obj.Id;
        
       DatabaseService.Execute("DELETE FROM [dbo].[Order] WHERE ID = " + orderId);
       
       return true;
    }

    public static bool Update(Order obj)
    {
        int orderId = obj.Id;
        string q = "status = " + obj.Status + ", User_id = " + obj.User_id + ", bill_id = " + obj.Bill_id +
                   "WHERE Id = " + orderId;
        
        DatabaseService.Execute("UPDATE [dbo].[Order] SET " + q);
        
        return true;
    }

    public static bool Create(Order obj)
    {
        string q = obj.Status + ", " + obj.User_id + ", " + obj.Bill_id;
        
        DatabaseService.Execute("INSERT INTO [dbo].[Order] (status, User_id, bill_id) VALUES (" + q +")");
        
        return true;
    }
}