

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

        public void UpdateOrder(Order order)
        {
            _orderService.Update(order);
        }
        
        public Order GetOrder(int id)
        {
            return _orderService.FindByID(id);
        }
        
        public List<Order> PaymentAllowedForBill(int id)
        {
            return _orderService.PaymentTest(id);
        }

        public List<Order> GetOpenOrders()
        {
            return _orderService.GetAllOpenOrders();
        }
        
        public List<OrderLineProduct> GetOrderLines(int orderId)
        {
            return _orderLineService.GetAllById(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderService.GetAll();
        }
        
        public List<OrderLine> GetOrderLines()
        {
            return _orderLineService.GetAll();
        }
    }
}
