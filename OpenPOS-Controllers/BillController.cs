using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_APP.Controllers
{
    public static class BillController
    {
        private static BillService _billService = new();
        public static Bill CreateBill(int user_id)
        {
            Bill bill = new Bill() { User_id = user_id, Paid = false, Created_at = DateTime.Now, Updated_at = DateTime.Now };
            ApplicationSettings.CurrentBill = _billService.Create(bill);
            return bill;
        }
    }
}
