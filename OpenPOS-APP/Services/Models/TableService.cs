using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Interfaces;

namespace OpenPOS_APP.Services.Models;

public class TableService : IModelService<Table>
{
    public static List<Table> GetAll()
    {
        List<Table> resultList = DatabaseService.Execute<Table>("SELECT * FROM [dbo].[restaurant_table]");

        return resultList;
    }

    public static Table FindByID(int id)
    {
        Table result = DatabaseService.ExecuteSingle<Table>("SELECT * FROM [dbo].[restaurant_table] WHERE id = '" + id + "'");

        return result;
    }

    public static bool Delete(Table obj)
    {
        int tableId = obj.Id;
        DatabaseService.Execute("DELETE FROM [dbo].[restaurant_table] WHERE id = '" + tableId + "'");

        return true;
    }

    public static bool Update(Table obj)
    {
        int tableId = obj.Id;

        string q = "table_number = '" + obj.Table_number + "', bill_id = '" + obj.Bill_id + "', floor_id = '" +
                   obj.Floor_id + "' WHERE id = '" + tableId + "'";

        DatabaseService.Execute("UPDATE [dbo].[restaurant_table] SET " + q );

        return true;
    }

    public static bool Create(Table obj)
    {
        string q = "'" + obj.Table_number + "', '" + obj.Bill_id + "', '" + obj.Floor_id + "'";

        DatabaseService.Execute("INSERT INTO [dbo].[restaurant_table] (table_number, bill_id, floor_id) VALUES (" + q + ")");

        return true;
    }
}