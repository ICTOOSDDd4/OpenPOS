using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
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

    /// <summary>
    /// Returns all Tables from database
    /// </summary>
    /// <returns>All Tables in list of models</returns>
    public List<Table> GetAll()
    {
        List<Table> resultList = DatabaseService.Execute<Table>(new SqlCommand("SELECT * FROM [dbo].[restaurant_table]"));

        return resultList;
    }

    /// <summary>
    /// Returns a Table by id
    /// </summary>
    /// <param name="id">TableId</param>
    /// <returns>Table</returns>
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

    /// <summary>
    /// Deletes the Table given by id
    /// </summary>
    /// <param name="obj">Table model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Delete(Table obj)
    {
        SqlCommand query = new SqlCommand("DELETE FROM [dbo].[restaurant_table] WHERE [ID] = @ID");

        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Updates the Table by given id and other data
    /// </summary>
    /// <param name="obj">Table model</param>
    /// <returns>Bool for succeeded or not</returns>
    public bool Update(Table obj)
    {
        SqlCommand query = new SqlCommand("UPDATE [dbo].[restaurant_table] SET [table_number] = @TableNumber, [bill_id] = @BillId, [floor_id] = @FloorId WHERE [id] = @ID");

        query.Parameters.Add("@TableNumber", SqlDbType.Int);
        query.Parameters["@TableNumber"].Value = obj.Table_number;
        query.Parameters.Add("@BillId", SqlDbType.Int);
        query.Parameters["@BillId"].Value = (object)obj.Bill_id ?? DBNull.Value;
        query.Parameters.Add("@FloorId", SqlDbType.Int);
        query.Parameters["@FloorId"].Value = obj.Floor_id;
        query.Parameters.Add("@ID", SqlDbType.Int);
        query.Parameters["@ID"].Value = obj.Id;

        return DatabaseService.Execute(query);
    }

    /// <summary>
    /// Creates a new Table with given data
    /// </summary>
    /// <param name="obj">Table model</param>
    /// <returns>Updated Table model</returns>
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
