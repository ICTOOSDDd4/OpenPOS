namespace OpenPOS_APP.Models
{
    public class Floor
    {
        public int Id { get; set; }
        public string Storey { get; set; }
        public Floor() { }
        public Floor(int id, string storey)
        {
            Id = id;
            Storey = storey;
        }
    }
}
