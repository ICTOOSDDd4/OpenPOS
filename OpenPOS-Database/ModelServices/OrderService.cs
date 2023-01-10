using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices
{

    public class OrderService : IModelService<Order>
    {
        /// <summary>
        /// Returns all Orders from database
        /// </summary>
        /// <returns>All Orders in list of models</returns>
        public List<Order> GetAll()
        {
            List<Order> resultList = DatabaseService.Execute<Order>(new SqlCommand("SELECT * FROM [dbo].[Order]"));

            return resultList;
        }

        /// <summary>
        /// Returns all Orders that have not been completed (status = false) from database
        /// </summary>
        /// <returns>All Orders in list of models</returns>
        public List<Order> GetAllOpenOrders()
        {
            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE [status] = @status");
            query.Parameters.Add("@status", SqlDbType.TinyInt);
            query.Parameters["@status"].Value = false;

            return DatabaseService.Execute<Order>(query);
        }

        /// <summary>
        /// Returns a Order by id
        /// </summary>
        /// <param name="id">OrderId</param>
        /// <returns>Order</returns>
        public Order FindByID(int id)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE [Id] = @ID");

            query.Parameters.Add("@ID", SqlDbType.Int);
            query.Parameters["@ID"].Value = id;

            Order result = DatabaseService.ExecuteSingle<Order>(query);

            return result;
        }

        /// <summary>
        /// Returns all Orders that have not been completed (status = false) that are linked to a Bill
        /// </summary>
        /// <param name="billId">BillId</param>
        /// <returns>All Orders in list of models</returns>
        public List<Order> PaymentTest(int billId)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[Order] WHERE [bill_id] = @bill_id WHERE [status] = false");

            query.Parameters.Add("@ID", SqlDbType.Int);
            query.Parameters["@bill_id"].Value = billId;

            return DatabaseService.Execute<Order>(query);
        }

        /// <summary>
        /// Deletes the Delete given by id
        /// </summary>
        /// <param name="obj">Delete model</param>
        /// <returns>Bool for succeeded or not</returns>
        public bool Delete(Order obj)
        {
            SqlCommand query = new SqlCommand("DELETE FROM [dbo].[order] WHERE [ID] = @OrderId");

            query.Parameters.Add("@OrderId", SqlDbType.Int);
            query.Parameters["@OrderId"].Value = obj.Id;

            return DatabaseService.Execute(query);
        }

        /// <summary>
        /// Updates the Order by given id and other data
        /// </summary>
        /// <param name="obj">Order model</param>
        /// <returns>Bool for succeeded or not</returns>
        public bool Update(Order obj)
        {
            SqlCommand query =
                new SqlCommand(
                    "UPDATE [dbo].[order] SET [status] = @status, [user_id] = @userid, [bill_id] = @billId WHERE [id] = @id");

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

        /// <summary>
        /// Creates a new Order with given data
        /// </summary>
        /// <param name="obj">Order model</param>
        /// <returns>Updated Order model</returns>
        public Order Create(Order obj)
        {
            SqlCommand query =
                new SqlCommand(
                    "INSERT INTO [dbo].[order] ([status], [user_id], [bill_id], [created_at], [updated_at])  OUTPUT  inserted.*  VALUES (@status, @userid, @billId, @created_at, @updated_at)");

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
}