using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class BillServiceTest
{
    private Bill _bill;
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
    public void BillService_GetAllbills_ReturnsAllbills()
    {
        var bills = BillService.GetAll();
        Assert.Greater(bills.Count, 1);
    }

    [Test]
    public void BillService_Createbill_ReturnsObject()
    {

        var bill = new Bill()
        {
            Paid = true,
            User_id = 1,
            Created_at = DateTime.Now,
            Updated_at = DateTime.Now
        };
        var result = BillService.Create(bill);
        _bill = result;
        Assert.AreEqual(bill.User_id, result.User_id);
    }

    [Test]
    public void BillService_Findbill_Returnsbill()
    {
        BillService_Createbill_ReturnsObject();
        var bill = BillService.FindByID(_bill.Id);
        Assert.AreEqual(_bill.User_id, bill.User_id);
        BillService_Deletebill_ReturnsTrue();
    }

    [Test]
    public void BillService_Updatebill_ReturnsTrue()
    {
        Bill bill = BillService.FindByID(_bill.Id);
        bill.Paid = false;
        var result = BillService.Update(bill);
        Assert.IsTrue(result);
    }


    [Test]
    public void BillService_Deletebill_ReturnsTrue()
    {
        var bill = BillService.FindByID(_bill.Id);
        var result = BillService.Delete(bill);
        Assert.IsTrue(result);
    }
}
