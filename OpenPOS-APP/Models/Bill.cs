namespace OpenPOS_APP.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public bool Paid { get; set; }
        
        public Bill(int id, int user_id, bool paid)
        {
            Id = id;
            User_id = user_id;
            Paid = paid;
        }
    }
}
