﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int Table_number { get; set; }
        public int Bill_id { get; set; }
        public int Floor_id { get; set; }

        public Table(int id, int tableNumber, int billId, int floorId)
        {
            Id = id;
            Table_number = tableNumber;
            Bill_id = billId;
            Floor_id = floorId;
        }
    }
}