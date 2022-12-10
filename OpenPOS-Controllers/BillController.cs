using OpenPOS_Database.ModelServices;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_Controllers
{
    public static class BillController
    {
        private static BillService _billService = new();
        
        // Create Method
        public static Bill CreateBill(int user_id)
        {
            Bill bill = new Bill()
                { User_id = user_id, Paid = false, Created_at = DateTime.Now, Updated_at = DateTime.Now };
            ApplicationSettings.CurrentBill = _billService.Create(bill);
            return bill;
        }
        
        // Delete Method
        public static bool Delete(Bill bill)
        {
            return _billService.Delete(bill);
        }
        
        // Select Methods
        public static List<Bill> GetAll()
        {
            return _billService.GetAll();
        }

        public static Bill Find(int id)
        {
            return _billService.FindByID(id);
        }
        
        // Update Methods

        public static bool UpdateAll(Bill bill)
        {
            return _billService.Update(bill);
        }
        
        public static bool MarkAsPaid(Bill bill) // Update Using object
        {
            bill.Paid = true;
            return _billService.Update(bill);
        }
        
        public static bool MarkAsPaid(int id) // Update using id
        {
            Bill bill = Find(id);
            bill.Paid = true;
            return _billService.Update(bill);
        }

        public static bool UpdateAttachedUser(int bilId, int userId) // Update using id and id
        {
            Bill bill = Find(bilId);
            bill.User_id = userId;
            return _billService.Update(bill);
        }
        
        public static bool UpdateAttachedUser(Bill bill, int userId) // Update using object and id
        {
            bill.User_id = userId;
            return _billService.Update(bill);
        }
        
        public static bool UpdateAttachedUser(Bill bill, User user) // Update using object and object
        {
            bill.User_id = user.Id;
            return _billService.Update(bill);
        }
        
    }
}
