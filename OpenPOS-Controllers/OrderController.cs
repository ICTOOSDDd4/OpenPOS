using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Controllers
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
