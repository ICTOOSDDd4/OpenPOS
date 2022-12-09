using System.Data;
using OpenPOS_APP.Services.Interfaces;
using System.Data.SqlClient;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models;

public class TableService : IModelService<Table>
{

    public Table FindByTableNumber(int tableNumber) 
   {
      SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[restaurant_table] WHERE [table_number] = @TableNumber");

      query.Parameters.Add("@TableNumber", SqlDbType.Int);
      query.Parameters["@TableNumber"].Value = tableNumber;

      Table result = DatabaseService.ExecuteSingle<Table>(query);

      if (result.Table_number == 0)
      {
          return null;
      }
      
      return result;
   }
    public List<Table> GetAll()
    {
        List<Table> resultList = DatabaseService.Execute<Table>(new SqlCommand("SELECT * FROM [dbo].[restaurant_table]"));

        return resultList;
    }

    public Table FindByID(int id)
    {
        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[restaurant_table] WHERE [ID] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = id;

        Table result = DatabaseService.ExecuteSingle<Table>(query);

        return result;
    }

    public Table FindByBill(int id)
    {

        SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[restaurant_table] WHERE [bill_Id] = @BillId");

        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = id;

        Table result = DatabaseService.ExecuteSingle<Table>(query);

        return result;
    }

    public bool Delete(Table obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[restaurant_table] WHERE [ID] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    public bool Update(Table obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[restaurant_table] SET [table_number] = @TableNumber, [bill_id] = @BillId, [floor_id] = @FloorId WHERE [id] = @ID");

        query.Parameters.Add("@TableNumber", SqlDbType.Int);
        query.Parameters["@TableNumber"].Value = obj.Table_number;
        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = obj.Bill_id;
        query.Parameters.Add("@FloorId", SqlDbType.Int);
        query.Parameters["@FloorId"].Value = obj.Floor_id;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    public Table Create(Table obj)
    {
        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[restaurant_table] ([table_number], [bill_id], [floor_id])  OUTPUT  inserted.*  VALUES (@TableNumber, @BillId, @FloorId)");

        query.Parameters.Add("@TableNumber", SqlDbType.Int);
        query.Parameters["@TableNumber"].Value = obj.Table_number;
        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = obj.Bill_id;
        query.Parameters.Add("@FloorId", SqlDbType.Int);
        query.Parameters["@FloorId"].Value = obj.Floor_id;

        return DatabaseService.ExecuteSingle<Table>(query);
    }
}
