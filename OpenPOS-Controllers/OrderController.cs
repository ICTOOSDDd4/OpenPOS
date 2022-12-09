using OpenPOS_APP.Models;
using OpenPOS_Database.Services.Models;

namespace OpenPOS_Controllers
{
    public class OrderController
    {
        public void CreateOrder(Order order, List<OrderLine> lines)
        {
            OrderService.Create(order);
            foreach(var line in lines)
            {
                OrderLineService.Create(line);
            }
        }
    }
}
