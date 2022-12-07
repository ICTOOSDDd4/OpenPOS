using NUnit.Framework;
using OpenPOS_APP.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class BillModelTest
{
    [Test]
    public void Bill_NewBill_ReturnsBill()
    {
        int id = 2;
        int user_id = 1;
        bool paid = true;
        DateTime created_at = DateTime.Today;
        DateTime updated_at = DateTime.Today;
        
        Bill bill = new Bill(id, user_id, paid, created_at, updated_at);
        
        Assert.AreEqual(id, bill.Id);
        Assert.AreEqual(user_id, bill.User_id);
        Assert.AreEqual(paid, bill.Paid);
        Assert.AreEqual(created_at, bill.Created_at);
        Assert.AreEqual(created_at, bill.Updated_at);
        
    }
}