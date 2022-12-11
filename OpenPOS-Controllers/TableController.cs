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
    public class TableController
    {
        private TableService _tableService;

        public TableController()
        {
            _tableService = new TableService();
        }
        public bool CreateTableAsync(int tableId)
        {
            BillController billController = new BillController();
            //ApplicationSettings.TableNumber = _tableNumber;
            Table table = _tableService.FindByTableNumber(tableId);
            if (table == null)
            {
                return false;
            }
            else
            {
                ApplicationSettings.CurrentBill = billController.CreateBill(ApplicationSettings.LoggedinUser.Id);

                table.Bill_id = ApplicationSettings.CurrentBill.Id;

                _tableService.Update(table);
                return true;
            }    
        }
        public Table GetTable(Order order)
        {
            return _tableService.FindByBill(order.Bill_id);
        }
    }
}
