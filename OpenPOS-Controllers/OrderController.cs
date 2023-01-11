

using OpenPOS_API;
using OpenPOS_Database.ModelServices;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers
{
    public class OrderController
    {
        private OrderService _orderService;
        private OrderLineService _orderLineService;
        private OpenPosApiService _openPosApiService;

        public OrderController()
        {
            _orderLineService = new OrderLineService();
            _orderService = new OrderService();
            _openPosApiService = new OpenPosApiService();
        }

        /// <summary>
        /// Creates a new Order with OrderLines based by selectedProducts
        /// </summary>
        /// <param name="selectedProducts">All the current items in the shoppingcart</param>
        /// <returns>Bool for succeeded or not</returns>
        public async Task<bool> CreateOrder(Dictionary<int, int> selectedProducts)
        {
            try
            {
                Order order = new Order() { User_id = ApplicationSettings.LoggedinUser.Id, Bill_id = ApplicationSettings.CurrentBill.Id, Status = false, Updated_At = DateTime.Now, Created_At = DateTime.Now };
                order = _orderService.Create(order);
                if (order == null)
                    return false;
                await _openPosApiService.NewOrderRequest(order);
                foreach (KeyValuePair<int, int> entry in selectedProducts)
                {
                    OrderLine line = new OrderLine(order.Id, entry.Key, entry.Value, "In Development");
                    _orderLineService.Create(line);
                }
                return true;
            } catch
            {
                return false;
            }
        }

        /// <summary>
        /// Updates Order by the new data given
        /// </summary>
        /// <param name="order">The new data for the Order</param>
        public void UpdateOrder(Order order)
        {
            _orderService.Update(order);
        }
        
        /// <summary>
        /// Gets an Order by Id given
        /// </summary>
        /// <param name="id">OrderId needed</param>
        /// <returns>Order Model</returns>
        public Order GetOrder(int id)
        {
            return _orderService.FindByID(id);
        }

        /// <summary>
        /// Gets all open Orders
        /// </summary>
        /// <returns>List of Orders that are open (status = 0)</returns>
        public List<Order> GetOpenOrders()
        {
            return _orderService.GetAllOpenOrders();
        }
        
        /// <summary>
        /// Gets all OrderLines by OrderId
        /// </summary>
        /// <param name="orderId">OrderId of OrderLines needed</param>
        /// <returns>List of OrderLineProducts Needed for the Order</returns>
        public List<OrderLineProduct> GetOrderLines(int orderId)
        {
            return _orderLineService.GetAllById(orderId);
        }

        /// <summary>
        /// Gets all Orders
        /// </summary>
        /// <returns>List of all Orders</returns>
        public List<Order> GetAllOrders()
        {
            return _orderService.GetAll();
        }
        
        /// <summary>
        /// Gets all OrderLines
        /// </summary>
        /// <returns>List of all OrderLines</returns>
        public List<OrderLine> GetOrderLines()
        {
            return _orderLineService.GetAll();
        }
    }
}
