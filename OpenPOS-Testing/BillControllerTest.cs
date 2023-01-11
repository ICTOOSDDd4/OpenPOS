using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Controllers;

namespace OpenPOS_Testing;

[TestFixture]
public class BillControllerTest
{
    private BillController _billController = new();

    Bill _bill = new Bill
    {
        User_id = 1297,
        Paid = false,
        Created_at = DateTime.Today,
        Updated_at = DateTime.Now,
    };
    
    [SetUp]
    public void SetUp()
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("OpenPOS_Testing.appsettings.test.json");

        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            
            ApplicationSettings.DbSett = config.GetRequiredSection("DATABASE_CONNECTION").Get<DatabaseSettings>();
            
            if (ApplicationSettings.DbSett != null)
            {
                System.Diagnostics.Debug.WriteLine(ApplicationSettings.DbSett.connection_string);
            }
        }
        else throw new Exception();
        
      DatabaseService.Initialize();
    }
  
    [Test]
    public void BillController_GetAllBills_ReturnsAllBills()
    {
        var bill = _bill;
        var result = _billController.CreateBill(bill.User_id);
        var bills = _billController.GetAll();
        _billController.Delete(result);

        Assert.Greater(bills.Count, 0);
    }

    [Test]
    public void BillController_CreateBill_ReturnsObject()
    {

        var bill = _bill;
        var result = _billController.CreateBill(bill.User_id);
        _billController.Delete(result);

        Assert.That(bill.User_id, Is.EqualTo(result.User_id));
    }
    
    [Test]
    public void BillController_FindBill_ReturnsBill()
    {
        var bill = _bill;
        var createdBill = _billController.CreateBill(bill.User_id);
        var result = _billController.Find(createdBill.Id);
        _billController.Delete(result);

        Assert.That(bill.User_id, Is.EqualTo(result.User_id));
    }

    [Test]
    public void BillController_UpdateBill_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billController.CreateBill(bill.User_id);
        
        Assert.That(createdBill.User_id, Is.Not.EqualTo(1298));
        createdBill.User_id = 1298;
        
        var result = _billController.UpdateAll(createdBill);
        int id = _billController.Find(createdBill.Id).User_id;
        _billController.Delete(createdBill);

        Assert.IsTrue(result);
        Assert.That(id, Is.EqualTo(1298));
    }

    [Test]
    public void BillController_DeleteBill_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billController.CreateBill(bill.User_id);
        var result = _billController.Delete(createdBill);
        
        Assert.IsTrue(result);
        Assert.That(_billController.Find(createdBill.Id).User_id, Is.EqualTo(0));
    }

    [Test]
    public void BillController_MarkAsPaid_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billController.CreateBill(bill.User_id);

        _billController.MarkAsPaid(createdBill);
        Assert.IsTrue(_billController.Find(createdBill.Id).Paid);

        createdBill.Paid = false;
        _billController.UpdateAll(createdBill);
        Assert.IsTrue(_billController.MarkAsPaid(createdBill.Id));
        
        _billController.Delete(createdBill);
    }

    [Test]
    public void BillController_UpdateUser_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billController.CreateBill(bill.User_id);

        Assert.That(createdBill.User_id, Is.Not.EqualTo(1298));

        Assert.IsTrue(_billController.UpdateAttachedUser(createdBill.Id, 1298));
        Assert.That(_billController.Find(createdBill.Id).User_id, Is.EqualTo(1298));
        
        Assert.IsTrue(_billController.UpdateAttachedUser(createdBill, 1297));
        Assert.That(_billController.Find(createdBill.Id).User_id, Is.EqualTo(1297));

        var user = new User() { Id = 1298 };
        Assert.IsTrue(_billController.UpdateAttachedUser(createdBill, user));
        Assert.That(_billController.Find(createdBill.Id).User_id, Is.EqualTo(1298));

        _billController.Delete(createdBill);
    }
}