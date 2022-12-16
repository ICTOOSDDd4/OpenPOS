using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing;

[TestFixture]
public class BillServiceTest
{
    private BillService _billService = new();

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
    public void BillService_GetAllBills_ReturnsAllBills()
    {
        var bill = _bill;
        var result = _billService.Create(bill);
        var bills = _billService.GetAll();
        _billService.Delete(result);

        Assert.Greater(bills.Count, 0);
    }

    [Test]
    public void BillService_CreateBill_ReturnsObject()
    {

        var bill = _bill;
        var result = _billService.Create(bill);
        _billService.Delete(result);

        Assert.That(bill.User_id, Is.EqualTo(result.User_id));
    }
    
    [Test]
    public void BillService_FindBill_ReturnsBill()
    {
        var bill = _bill;
        var createdBill = _billService.Create(bill);
        var result = _billService.FindByID(createdBill.Id);
        _billService.Delete(result);

        Assert.That(bill.User_id, Is.EqualTo(result.User_id));
    }

    [Test]
    public void BillService_UpdateBill_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billService.Create(bill);
        
        Assert.That(createdBill.User_id, Is.Not.EqualTo(239));
        createdBill.User_id = 1298;
        
        var result = _billService.Update(createdBill);
        _billService.Delete(createdBill);

        Assert.IsTrue(result);
        Assert.That(_billService.FindByID(createdBill.Id).User_id, Is.EqualTo(1298));
    }

    [Test]
    public void BillService_DeleteBill_ReturnsTrue()
    {
        var bill = _bill;
        var createdBill = _billService.Create(bill);
        var result = _billService.Delete(createdBill);
        
        Assert.IsTrue(result);
        Assert.That(_billService.FindByID(createdBill.Id).User_id, Is.EqualTo(0));
    }
}