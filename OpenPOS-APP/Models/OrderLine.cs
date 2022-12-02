using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Models
{
    public class OrderLine
    {
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public bool Status { get; set; } 
        public int Amount { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime Created_at { get; set; } 

        public OrderLine() { }
        public OrderLine(int order_id, int product_id, bool status, int amount, string comment) 
        {
            Order_id = order_id;
            Product_id = product_id;
            Status = status;
            Amount = amount;
            Comment = comment;
        }
    }
}
