using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
