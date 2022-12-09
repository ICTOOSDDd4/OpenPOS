using Microsoft.Extensions.Configuration;
using OpenPOS_Models;
using OpenPOS_APP.Services;
using OpenPOS_Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderLineServiceTest
{
    private OrderLineService _orderLineService = new();

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
        var result = _orderLineService.Create(OrderLine);
        var OrderLines = _orderLineService.GetAll();

        Assert.Greater(OrderLines.Count, 0);

        _orderLineService.Delete(result);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var OrderLine = this.OrderLine;
        var result = _orderLineService.Create(OrderLine);

        Assert.That(OrderLine.Comment, Is.EqualTo(result.Comment));

        _orderLineService.Delete(result);
    }

    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var OrderLine = this.OrderLine;
        var createdOrderLine = _orderLineService.Create(OrderLine);
        var result = _orderLineService.GetAllById(createdOrderLine.Order_id);

        Assert.Greater(result.Count, 0);

        foreach(var line in result) 
        { 
            _orderLineService.Delete(line);
        }
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var OrderLine = this.OrderLine;
        var createdOrderLine = _orderLineService.Create(OrderLine);
        var result = _orderLineService.Delete(createdOrderLine);

        Assert.IsTrue(result);
        Assert.IsEmpty(_orderLineService.GetByIds(createdOrderLine.Order_id, createdOrderLine.Product_id));
    }

    [Test]
    public void OrderService_GetAllUnfinishedOrders_ReturnsAllUnfinishedOrders()
    {
        var OrderLine = this.OrderLine;
        var OrderLines = _orderLineService.GetAllUnfinished();

        Assert.Greater(_orderLineService.GetAll().Count, OrderLines.Count);
    }
}