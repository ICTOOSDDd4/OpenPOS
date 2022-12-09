using OpenPOS_Database.ModelServices;
using OpenPOS_Models;
using OpenPOS_Settings;

namespace OpenPOS_Controllers
{
    public static class BillController
    {
        private static BillService _billService = new();

        public static Bill CreateBill(int user_id)
        {
            Bill bill = new Bill()
                { User_id = user_id, Paid = false, Created_at = DateTime.Now, Updated_at = DateTime.Now };
            ApplicationSettings.CurrentBill = _billService.Create(bill);
            return bill;
        }
        
        public static List<Bill> GetAllBills()
        {
            return _billService.GetAll();
        }

        public static bool UpdateBill(Bill bill)
        {
            return _billService.Update(bill);
        }
        
        public static Bill FindBill(int id)
        {
            return _billService.FindByID(id);
        }
        
        public static bool DeleteBill(Bill bill)
        {
            return _billService.Delete(bill);
        }

    }
}
