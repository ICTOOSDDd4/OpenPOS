using System.Data.SqlClient;
using System.Data;
using OpenPOS_Models;

namespace OpenPOS_Database.Services.Models
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
    }
}
