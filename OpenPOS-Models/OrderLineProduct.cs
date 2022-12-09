namespace OpenPOS_Models;
{
    public class OrderLineProduct
    {
        public int Order_id { get; set; }
        public int Product_id { get; set; }
        public bool Status { get; set; }
        public int Amount { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime Created_at { get; set; }

        public OrderLineProduct() { }
        public OrderLineProduct(int order_id, int product_id, bool status, int amount, string comment)
        {
            Order_id = order_id;
            Product_id = product_id;
            Status = status;
            Amount = amount;
            Comment = comment;
        }
    }
}