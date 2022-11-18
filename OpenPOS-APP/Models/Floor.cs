using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Models
{
    public class Floor
    {
        public int Id { get; set; }
        public string Storey { get; set; }

        public Floor(int id, string storey)
        {
            Id = id;
            Storey = storey;
        }
    }
}
