using Microsoft.Extensions.Configuration;
using OpenPOS_Database.ModelServices;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderLineServiceTest
{
    private OrderLineService _orderLineService = new();

    private OrderLine _orderLine = new OrderLine
    {
        Order_id = 1143,
        Product_id = 1168,
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
        var orderLine = _orderLine;
        var result = _orderLineService.Create(orderLine);
        var orderLines = _orderLineService.GetAll();
        _orderLineService.Delete(result);

        Assert.Greater(orderLines.Count, 0);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var orderLine = this._orderLine;
        var result = _orderLineService.Create(orderLine);
        _orderLineService.Delete(result);
        
        Assert.That(orderLine.Comment, Is.EqualTo(result.Comment));
    }

    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var orderLine = this._orderLine;
        var createdOrderLine = _orderLineService.Create(orderLine);
        var result = _orderLineService.GetAllById(createdOrderLine.Order_id);
        foreach(var line in result) 
        { 
            _orderLineService.Delete(line);
        }
        
        Assert.Greater(result.Count, 0);
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var orderLine = this._orderLine;
        var createdOrderLine = _orderLineService.Create(orderLine);
        var result = _orderLineService.Delete(createdOrderLine);

        Assert.IsTrue(result);
        Assert.IsEmpty(_orderLineService.GetByIds(createdOrderLine.Order_id, createdOrderLine.Product_id));
    }

    [Test]
    public void OrderService_GetAllUnfinishedOrders_ReturnsAllUnfinishedOrders()
    {
        var orderLines = _orderLineService.GetAllUnfinished();

        Assert.Greater(_orderLineService.GetAll().Count, orderLines.Count);
    }
}