using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderServiceTest
{
    private OrderService _orderService = new();

    private Order _order = new Order
    {
        User_id = 1297,
        Bill_id = 1221,
        Status = false,
        Updated_At = DateTime.Today,
        Created_At = DateTime.Now
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
        var order = this._order;
        var result = _orderService.Create(order);
        var orders = _orderService.GetAll();
        
        Assert.Greater(orders.Count, 0);
        
        _orderService.Delete(result);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var order = this._order;
        var result = _orderService.Create(order);
        _orderService.Delete(result);

        
        Assert.That(order.User_id, Is.EqualTo(result.User_id));
    }
    
    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var order = this._order;
        var createdOrder = _orderService.Create(order);
        var result = _orderService.FindByID(createdOrder.Id);
        _orderService.Delete(result);

        Assert.That(createdOrder.User_id, Is.EqualTo(result.User_id));
    }

    [Test]
    public void OrderService_UpdateOrder_ReturnsTrue()
    {
        var order = this._order;
        var createdOrder = _orderService.Create(order);
        
        Assert.That(createdOrder.User_id, Is.Not.EqualTo(1298));
        createdOrder.User_id = 1298;
        
        var result = _orderService.Update(createdOrder);
        int id = _orderService.FindByID(createdOrder.Id).User_id;
        _orderService.Delete(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(id, Is.EqualTo(1298));
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var order = this._order;
        var createdOrder = _orderService.Create(order);
        var result = _orderService.Delete(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(_orderService.FindByID(createdOrder.Id).User_id, Is.EqualTo(0));
    }
}