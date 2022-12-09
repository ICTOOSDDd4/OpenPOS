namespace OpenPOS_Models;
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Role() { }

        public Role(string title)
        {
            Title = title;
        }
        public Role(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
