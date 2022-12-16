

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
                _openPosApiService.NewOrderRequest(order);
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

        public List<Order> GetOpenOrders()
        {
            return _orderService.GetAllOpenOrders();
        }
        
        public List<OrderLineProduct> GetOrderLines(int orderId)
        {
            return _orderLineService.GetAllById(orderId);
        }

        public bool OrderLinesToDone(List<OrderLineProduct> lines, Order order)
        {
            try
            {
                foreach(OrderLineProduct line in lines)
                {
                    line.Status = true;
                    _orderLineService.ChangeStatus(line);
                }

                if (_orderLineService.GetAllOpenById(order.Id).Count == 0)
                {
                    order.Status = true;
                    UpdateOrder(order);
                }

                return true;
            } catch 
            {
                return false; 
            }
        }
    }
}
