using System.Data;
using System.Data.SqlClient;
using OpenPOS_Database.Interfaces;
using OpenPOS_Models;

namespace OpenPOS_Database.ModelServices
{
    public class OrderLineService
    {
        public List<OrderLine> GetAll()
        {
            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[order_product]");

            return DatabaseService.Execute<OrderLine>(query);
        }
        public List<OrderLineProduct> GetAllById(int id)
        {
            SqlCommand query = new SqlCommand("SELECT m.order_id, m.product_id, o.status, m.amount, p.name, m.comment, o.created_at FROM [OpenPOS_dev].[dbo].[order_product] as m INNER JOIN [dbo].[order] as o ON m.order_id = o.id INNER JOIN product as p ON m.product_id = p.id WHERE o.id = @ID");

            query.Parameters.Add("@ID", SqlDbType.Int);
            query.Parameters["@ID"].Value = id;

            List<OrderLineProduct> resultList = DatabaseService.Execute<OrderLineProduct>(query);

            return resultList;
        }
        public List<OrderLineProduct> GetAllOpenById(int id)
        {
            SqlCommand query = new SqlCommand("SELECT m.order_id, m.product_id, o.status, m.amount, p.name, m.comment, o.created_at FROM [OpenPOS_dev].[dbo].[order_product] as m INNER JOIN [dbo].[order] as o ON m.order_id = o.id INNER JOIN product as p ON m.product_id = p.id WHERE o.id = @ID AND m.status = @Status");

            query.Parameters.Add("@ID", SqlDbType.Int);
            query.Parameters["@ID"].Value = id;
            query.Parameters.Add("@Status", SqlDbType.TinyInt);
            query.Parameters["@Status"].Value = false;

            List<OrderLineProduct> resultList = DatabaseService.Execute<OrderLineProduct>(query);

            return resultList;
        }
        public List<OrderLine> GetByIds(int Order_id, int Product_id)
        {
            SqlCommand query = new SqlCommand("SELECT * FROM [dbo].[order_product] WHERE [order_id] = @OrderId AND [product_id] = @ProductId");

            query.Parameters.Add("@OrderId", SqlDbType.Int);
            query.Parameters["@OrderId"].Value = Order_id;
            query.Parameters.Add("@ProductId", SqlDbType.Int);
            query.Parameters["@ProductId"].Value = Product_id;

            List<OrderLine> resultList = DatabaseService.Execute<OrderLine>(query);

            return resultList;
        }
        public List<OrderLineProduct> GetAllUnfinished()
        {
            SqlCommand query = new SqlCommand("SELECT m.order_id, m.product_id, o.status, m.amount, p.name, m.comment, o.created_at FROM [OpenPOS_dev].[dbo].[order_product] as m INNER JOIN [dbo].[order] as o ON m.order_id = o.id INNER JOIN product as p ON m.product_id = p.id WHERE o.[status] = 0");

            List<OrderLineProduct> resultList = DatabaseService.Execute<OrderLineProduct>(query);

            return resultList;
        }

        public bool Delete(OrderLineProduct obj)
        {
            SqlCommand query = new SqlCommand("DELETE FROM [dbo].[order_product] WHERE [order_id] = @OrderId AND [product_id] = @ProductId");

            query.Parameters.Add("@OrderId", SqlDbType.Int);
            query.Parameters["@OrderId"].Value = obj.Order_id;
            query.Parameters.Add("@ProductId", SqlDbType.Int);
            query.Parameters["@ProductId"].Value = obj.Product_id;

            return DatabaseService.Execute(query);
        }

        public bool Delete(OrderLine obj)
        {
            SqlCommand query = new SqlCommand("DELETE FROM [dbo].[order_product] WHERE [order_id] = @OrderId AND [product_id] = @ProductId");

            query.Parameters.Add("@OrderId", SqlDbType.Int);
            query.Parameters["@OrderId"].Value = obj.Order_id;
            query.Parameters.Add("@ProductId", SqlDbType.Int);
            query.Parameters["@ProductId"].Value = obj.Product_id;

            return DatabaseService.Execute(query);
        }

        public OrderLine Create(OrderLine obj)
        {
            SqlCommand query = new SqlCommand("INSERT INTO [dbo].[order_product] ([order_id], [product_id], [amount], [comment])  OUTPUT  inserted.*  VALUES (@OrderId, @ProductId, @Amount, @Comment)");

            query.Parameters.Add("@OrderId", SqlDbType.Int);
            query.Parameters["@OrderId"].Value = obj.Order_id;
            query.Parameters.Add("@ProductId", SqlDbType.Int);
            query.Parameters["@ProductId"].Value = obj.Product_id;
            query.Parameters.Add("@Amount", SqlDbType.Int);
            query.Parameters["@Amount"].Value = obj.Amount;
            query.Parameters.Add("@Comment", SqlDbType.VarChar);
            query.Parameters["@Comment"].Value = obj.Comment;

            return DatabaseService.ExecuteSingle<OrderLine>(query);
        }

        public void ChangeStatus(OrderLineProduct obj)
        {
            SqlCommand query = new SqlCommand("UPDATE [dbo].[Order_product] SET [status] = @Status WHERE [order_id] = @OrderID AND [product_id] = @ProductID");

            query.Parameters.Add("@Status", SqlDbType.TinyInt);
            query.Parameters["@Status"].Value = obj.Status;
            query.Parameters.Add("@OrderID", SqlDbType.Int);
            query.Parameters["@OrderID"].Value = obj.Order_id;
            query.Parameters.Add("@ProductID", SqlDbType.Int);
            query.Parameters["@ProductID"].Value = obj.Product_id;

            DatabaseService.Execute(query);
        }
    }
}
