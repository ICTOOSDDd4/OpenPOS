using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPOS_Controllers 
{
    public static class TableController
    {
        private static TableService _tableService = new();
        public static bool CreateTableAsync(int tableId)
        {
            //ApplicationSettings.TableNumber = _tableNumber;
            Table table = _tableService.FindByTableNumber(tableId);
            if (table == null)
            {
                return false;
            }
            else
            {
                ApplicationSettings.CurrentBill = BillController.CreateBill(ApplicationSettings.LoggedinUser.Id);

                table.Bill_id = ApplicationSettings.CurrentBill.Id;

                _tableService.Update(table);
                return true;
            }    
        }
    }
}
