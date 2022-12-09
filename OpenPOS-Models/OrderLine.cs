namespace OpenPOS_Models
{
    public class OrderLine
    {
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }

        public OrderLine() { }
        public OrderLine(int order_id, int product_id, int amount, string comment) 
        {
            Order_id = order_id;
            Product_id = product_id;
            Amount = amount;
            Comment = comment;
        }
    }
}
