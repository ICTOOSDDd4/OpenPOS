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
        
        Assert.That(bill.Id, Is.EqualTo(id));
        Assert.That(bill.User_id, Is.EqualTo(user_id));
        Assert.That(bill.Paid, Is.EqualTo(paid));
        Assert.That(bill.Created_at, Is.EqualTo(created_at));
        Assert.That(bill.Updated_at, Is.EqualTo(created_at));
        
    }
}