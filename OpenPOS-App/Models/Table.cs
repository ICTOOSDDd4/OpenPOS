using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_App.Models
{
    internal class Table
    {
        public int Id { get; set; }
        public int Table_number { get; set; }
        public int Bill_id { get; set; }
        public int Floor_id { get; set; }
    }
}
