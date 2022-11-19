using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services.Models;

public class BillService : IModelService<Bill>
{
    public static List<Bill> GetAll()
    {
        List<Bill> resultList = DatabaseService.Execute<Bill>("SELECT * FROM [dbo].[Bill]");

        return resultList;
    }

    public static Bill FindByID(int id)
    {
        Bill result = DatabaseService.ExecuteSingle<Bill>("SELECT * FROM [dbo].[Bill] WHERE [ID] = " + id);
        
        return result;
    }

    public static bool Delete(Bill obj)
    {
        int billId = obj.Id;
        DatabaseService.Execute("DELETE FROM [dbo].[Bill] WHERE [ID] = " + billId);
        return true;
    }

    public static bool Update(Bill obj)
    {
        int billId = obj.Id;

        string q = "user_id = '" + obj.User_id + "', paid = '" + obj.Paid + "' WHERE id = " + billId;
        
        DatabaseService.Execute("UPDATE [dbo].[Bill] SET " + q);
        return true;
    }

    public static bool Create(Bill obj)
    {
        string q = "'" + obj.User_id + "', '" + obj.Paid + "'";
        DatabaseService.Execute("INSERT INTO [dbo].[Bill] (user_id, paid) VALUES (" + q + ")");
        return true;
    }
}