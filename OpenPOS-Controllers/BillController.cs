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
        // Create Method
        public Bill CreateBill(int user_id)
        {
            Bill bill = new Bill()
                { User_id = user_id, Paid = false, Created_at = DateTime.Now, Updated_at = DateTime.Now };
            ApplicationSettings.CurrentBill = _billService.Create(bill);
            return bill;
        }
        
        // Delete Method
        public bool Delete(Bill bill)
        {
            return _billService.Delete(bill);
        }
        
        // Select Methods
        public List<Bill> GetAll()
        {
            return _billService.GetAll();
        }

        public Bill Find(int id)
        {
            return _billService.FindByID(id);
        }
        
        // Update Methods

        public bool UpdateAll(Bill bill)
        {
            return _billService.Update(bill);
        }
        
        public bool MarkAsPaid(Bill bill) // Update Using object
        {
            bill.Paid = true;
            return _billService.Update(bill);
        }
        
        public bool MarkAsPaid(int id) // Update using id
        {
            Bill bill = Find(id);
            bill.Paid = true;
            return _billService.Update(bill);
        }

        public bool UpdateAttachedUser(int bilId, int userId) // Update using id and id
        {
            Bill bill = Find(bilId);
            bill.User_id = userId;
            return _billService.Update(bill);
        }
        
        public bool UpdateAttachedUser(Bill bill, int userId) // Update using object and id
        {
            bill.User_id = userId;
            return _billService.Update(bill);
        }
        
        public bool UpdateAttachedUser(Bill bill, User user) // Update using object and object
        {
            bill.User_id = user.Id;
            return _billService.Update(bill);
        }
        
    }
}
