using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace OpenPOS_APP.Services.Models;

public class BillService : IModelService<Bill>
{
    public static List<Bill> GetAll()
    {
        List<Bill> resultList = DatabaseService.Execute<Bill>(new SqlCommand("SELECT * FROM [dbo].[Bill]"));

        return resultList;
    }

    public static Bill FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Bill] WHERE [ID] = @ID");
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Bill result = DatabaseService.ExecuteSingle<Bill>(query);
        
        return result;
    }

    public static bool Delete(Bill obj)
    {
        int billId = obj.Id;
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[Bill] WHERE [ID] = @BillId");
        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = billId;
        DatabaseService.Execute(query);
        
        return true;
    }

    public static bool Update(Bill obj)
    {
        int billId = obj.Id;
        string q = "user_id = '" + obj.User_id + "', paid = '" + obj.Paid + "' WHERE id = " + billId;
        
        DatabaseService.Execute(new SqlCommand("UPDATE [dbo].[Bill] SET " + q));
        
        return true;
    }

    public static bool Create(Bill obj)
    {
        string q = "'" + obj.User_id + "', '" + obj.Paid + "'";
        
        DatabaseService.Execute(new SqlCommand("INSERT INTO [dbo].[Bill] (user_id, paid) VALUES (" + q + ")"));
        
        return true;
    }
}