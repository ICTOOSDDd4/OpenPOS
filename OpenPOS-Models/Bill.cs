namespace OpenPOS_APP.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public bool Paid { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        
        public Bill () { }
        public Bill(int id, int user_id, bool paid, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            User_id = user_id;
            Paid = paid;
            Created_at = createdAt;
            Updated_at = updatedAt;
        }
    }
}
