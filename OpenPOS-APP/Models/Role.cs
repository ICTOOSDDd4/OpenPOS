namespace OpenPOS_APP.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public Role(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
