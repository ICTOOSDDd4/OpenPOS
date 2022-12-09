namespace OpenPOS_Models;
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(string name, string lastName, string email, string password)
        {
            Name = name;
            Last_name = lastName;
            Email = email;
            Password = password;
        }
    }
}
