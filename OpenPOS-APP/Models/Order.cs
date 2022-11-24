﻿namespace OpenPOS_APP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Bill_id { get; set; }
        public bool Status { get; set; }
        public List<OrderLine> Lines { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Created_At { get; set; }
        public Order() { }
        public Order(int id, bool status, int userId, int billId, List<OrderLine> lines, DateTime updatedAt, DateTime createdAt)
        {
            Id = id;
            Status = status;
            User_id = userId;
            Bill_id = billId;
            Lines = lines;
            Updated_At = updatedAt;
            Created_At = createdAt;
        }
    }
}
