using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_App.Models
{
    internal class Bill
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public bool Paid { get; set; }
    }
}
