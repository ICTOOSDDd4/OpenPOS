using OpenPOS_APP.Models;
using OpenPOS_Database.Services.Models;
using OpenPOS_APP.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Controllers
{
    public static class OrderController
    {
        private static OrderService _orderService = new();
        private static OrderLineService _orderLineService = new();
        public static bool CreateOrder(Dictionary<int, int> SelectedProducts)
        {
            try
            {
                Order order = new Order() { User_id = ApplicationSettings.LoggedinUser.Id, Bill_id = ApplicationSettings.CurrentBill.Id, Status = false, Updated_At = DateTime.Now, Created_At = DateTime.Now };
                order = _orderService.Create(order);
                if (order == null)
                    return false;

                foreach (KeyValuePair<int, int> entry in SelectedProducts)
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

       
    }
}
