namespace OpenPOS_APP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Bill_id { get; set; }
        public bool Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Order() { }
        public Order(int id, bool status, int userId, int billId)
        {
            Id = id;
            Status = status;
            User_id = userId;
            Bill_id = billId;
        }
    }
}
