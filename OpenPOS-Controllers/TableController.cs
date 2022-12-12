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

        public bool CheckForOpenBill(int tableNumber)
        {
            if (_tableService.FindByTableNumber(tableNumber).Bill_id == null)
            {
                return false;
            }
            return true;
        }
        
        public bool AttachBillToTable(int tableId, int billId)
        {
            Table table = _tableService.FindByTableNumber(tableId);
            if (table == null)
            {
                return false;
            }
            table.Bill_id = billId;
            _tableService.Update(table);
            return true;
        }
        
        public Table GetByTableNumber(int tableNumber)
        {
            return _tableService.FindByTableNumber(tableNumber);
        }
        
        public Table GetByBillId(int billId)
        {
            return _tableService.FindByBill(billId);
        }

        public bool RemoveBill(int tableNumber)
        {
            Table table = _tableService.FindByTableNumber(tableNumber);
            table.Bill_id = null;
            return _tableService.Update(table);
        }
    }
}
