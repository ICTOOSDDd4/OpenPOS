using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderLineServiceTest
{
    OrderLine OrderLine = new OrderLine
    {
        Order_id = 61,
        Product_id = 56,
        Amount = 2,
        Comment = "test123"
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
    public void OrderService_GetAllOrders_ReturnsAllOrders()
    {
        var OrderLine = this.OrderLine;
        var result = OrderLineService.Create(OrderLine);
        var OrderLines = OrderLineService.GetAll();

        Assert.Greater(OrderLines.Count, 0);

        OrderLineService.Delete(result);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var OrderLine = this.OrderLine;
        var result = OrderLineService.Create(OrderLine);

        Assert.That(OrderLine.Comment, Is.EqualTo(result.Comment));

        OrderLineService.Delete(result);
    }

    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var OrderLine = this.OrderLine;
        var createdOrderLine = OrderLineService.Create(OrderLine);
        var result = OrderLineService.GetAllById(createdOrderLine.Order_id);

        Assert.Greater(result.Count, 0);

        foreach(var line in result) 
        { 
            OrderLineService.Delete(line);
        }
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var OrderLine = this.OrderLine;
        var createdOrderLine = OrderLineService.Create(OrderLine);
        var result = OrderLineService.Delete(createdOrderLine);

        Assert.IsTrue(result);
        Assert.IsEmpty(OrderLineService.GetByIds(createdOrderLine.Order_id, createdOrderLine.Product_id));
    }

    [Test]
    public void OrderService_GetAllUnfinishedOrders_ReturnsAllUnfinishedOrders()
    {
        var OrderLine = this.OrderLine;
        var OrderLines = OrderLineService.GetAllUnfinished();

        Assert.Greater(OrderLineService.GetAll().Count, OrderLines.Count);
    }
}