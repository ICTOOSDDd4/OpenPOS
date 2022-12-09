using Microsoft.Extensions.Configuration;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class _billServiceTest
{
    private BillService _billService = new();

    Bill Bill = new Bill
    {
        User_id = 176,
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
        var Bill = this.Bill;
        var result = _billService.Create(Bill);
        var Bills = _billService.GetAll();
        
        Assert.Greater(Bills.Count, 0);
        
        _billService.Delete(result);
    }

    [Test]
    public void BillService_CreateBill_ReturnsObject()
    {

        var Bill = this.Bill;
        var result = _billService.Create(Bill);
        
        Assert.That(Bill.User_id, Is.EqualTo(result.User_id));
        _billService.Delete(result);
    }
    
    [Test]
    public void BillService_FindBill_ReturnsBill()
    {
        var Bill = this.Bill;
        var createdBill = _billService.Create(Bill);
        var result = _billService.FindByID(createdBill.Id);

        Assert.That(Bill.User_id, Is.EqualTo(result.User_id));
       
        _billService.Delete(result);
    }

    [Test]
    public void BillService_UpdateBill_ReturnsTrue()
    {
        var Bill = this.Bill;
        var createdBill = _billService.Create(Bill);
        
        Assert.That(createdBill.User_id, Is.Not.EqualTo(239));
        createdBill.User_id = 239;
        
        var result = _billService.Update(createdBill);
        
        Assert.IsTrue(result);
        Assert.That(_billService.FindByID(createdBill.Id).User_id, Is.EqualTo(239));
        
        _billService.Delete(createdBill);
    }

    [Test]
    public void BillService_DeleteBill_ReturnsTrue()
    {
        var Bill = this.Bill;
        var createdBill = _billService.Create(Bill);
        var result = _billService.Delete(createdBill);
        
        Assert.IsTrue(result);
        Assert.That(_billService.FindByID(createdBill.Id).User_id, Is.EqualTo(0));
    }
}