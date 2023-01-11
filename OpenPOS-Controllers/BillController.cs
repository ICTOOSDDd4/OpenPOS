using OpenPOS_Database.ModelServices;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_Controllers
{
    public  class BillController
    {
        private BillService _billService;

        public BillController()
        {
            _billService = new BillService();
        }
        
        /// <summary>
        /// Creates an empty Bill linked to a User
        /// </summary>
        /// <param name="user_id">UserId needed to be assigned to the Bill</param>
        /// <returns>Bill model</returns>
        public Bill CreateBill(int user_id)
        {
            Bill bill = new Bill()
                { User_id = user_id, Paid = false, Created_at = DateTime.Now, Updated_at = DateTime.Now };
            return _billService.Create(bill); ;
        }
        
        /// <summary>
        /// Deletes a Bill by Bill model
        /// </summary>
        /// <param name="bill">Bill model</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool Delete(Bill bill)
        {
            return _billService.Delete(bill);
        }
        
        /// <summary>
        /// Gets all Bills
        /// </summary>
        /// <returns>List of all Bills</returns>
        public List<Bill> GetAll()
        {
            return _billService.GetAll();
        }

        /// <summary>
        /// Gets a Bill by BillId
        /// </summary>
        /// <param name="id">BillId</param>
        /// <returns>Bill model</returns>
        public Bill Find(int id)
        {
            return _billService.FindByID(id);
        }
        
        /// <summary>
        /// Updates a Bill to the given Bill model
        /// </summary>
        /// <param name="bill">Bill model</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool UpdateAll(Bill bill)
        {
            return _billService.Update(bill);
        }
        
        /// <summary>
        /// Changes Paid status of Bill to true by full model
        /// </summary>
        /// <param name="bill">Bill model</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool MarkAsPaid(Bill bill) 
        {
            bill.Paid = true;
            return _billService.Update(bill);
        }

        /// <summary>
        /// Changes Paid status of Bill to true by BillId
        /// </summary>
        /// <param name="id">BillId</param>
        /// <returns>Bool if succeeded or not</returns>
        public bool MarkAsPaid(int id)
        {
            Bill bill = Find(id);
            bill.Paid = true;
            return _billService.Update(bill);
        }

        /// <summary>
        /// Changes the Assigned User of a Bill to new UserId by BillId
        /// </summary>
        /// <param name="billId">BillId of Bill to be assigned to different User</param>
        /// <param name="userId">UserId of new User</param>
        /// <returns>Bool if succeeded of not</returns>
        public bool UpdateAttachedUser(int billId, int userId)
        {
            Bill bill = Find(billId);
            bill.User_id = userId;
            return _billService.Update(bill);
        }

        /// <summary>
        /// Changes the Assigned User of a Bill to new UserId by Bill model
        /// </summary>
        /// <param name="bill">Bill to be assigned to different User</param>
        /// <param name="userId">UserId of new User</param>
        /// <returns>Bool if succeeded of not</returns>
        public bool UpdateAttachedUser(Bill bill, int userId) 
        {
            bill.User_id = userId;
            return _billService.Update(bill);
        }

        /// <summary>
        /// Changes the Assigned User of a Bill to new User by Bill model
        /// </summary>
        /// <param name="bill">Bill to be assigned to different User</param>
        /// <param name="user">New User the Bill will be assigned to</param>
        /// <returns>Bool if succeeded of not</returns>
        public bool UpdateAttachedUser(Bill bill, User user)
        {
            bill.User_id = user.Id;
            return _billService.Update(bill);
        }
        
    }
}
