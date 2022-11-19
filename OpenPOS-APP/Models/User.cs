using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(int id, string name, string lastName, string email, string password)
        {
            Id = id;
            Name = name;
            Last_name = lastName;
            Email = email;
            Password = password;
        }
    }
}
