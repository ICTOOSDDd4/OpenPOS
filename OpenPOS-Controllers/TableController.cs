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

        /// <summary>
        /// Creates a new table with given tableNumber if it doesn't already exists
        /// </summary>
        /// <param name="tableNumber">TableNumber given by the input</param>
        /// <returns>Bool for succeeded or not</returns>
        public bool CreateTableAsync(int tableNumber)
        {
            BillController billController = new BillController();
            Table table = _tableService.FindByTableNumber(tableNumber);
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

        /// <summary>
        /// Checks if a Table has an open Bill assigned to it
        /// </summary>
        /// <param name="tableNumber">TableNumber that need to be checked</param>
        /// <returns>Bool for taken or not</returns>
        public bool CheckForOpenBill(int tableNumber)
        {
            if (_tableService.FindByTableNumber(tableNumber).Bill_id == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sets a Bill to a Table if it is not taken
        /// </summary>
        /// <param name="tableNumber">TableNumber that the Bill will be set to</param>
        /// <param name="billId">BillId that will get assigned to the Table</param>
        /// <returns>Bool for succeeded or not</returns>
        public bool AttachBillToTable(int tableNumber, int billId)
        {
            Table table = _tableService.FindByTableNumber(tableNumber);
            if (table == null)
            {
                return false;
            }
            table.Bill_id = billId;
            _tableService.Update(table);
            return true;
        }
        
        /// <summary>
        /// Finds a Table by its number
        /// </summary>
        /// <param name="tableNumber">TableNumber that needs to be found</param>
        /// <returns>Table model</returns>
        public Table GetByTableNumber(int tableNumber)
        {
            return _tableService.FindByTableNumber(tableNumber);
        }

        /// <summary>
        /// Finds a Table by its BillId
        /// </summary>
        /// <param name="billId">BillId that needs to be found</param>
        /// <returns>Table model</returns>
        public Table GetByBillId(int billId)
        {
            return _tableService.FindByBill(billId);
        }

        /// <summary>
        /// Unassignes a Bill from the Table by TableNumber
        /// </summary>
        /// <param name="tableNumber">TableNumber that the Bill will be unassigned from</param>
        /// <returns>Bool for succeeded or not</returns>
        public bool RemoveBill(int tableNumber)
        {
            Table table = _tableService.FindByTableNumber(tableNumber);
            table.Bill_id = null;
            return _tableService.Update(table);
        }
    }
}
