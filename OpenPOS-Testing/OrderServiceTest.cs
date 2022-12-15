using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderServiceTest
{
    private OrderService _orderService = new();

    Order Order = new Order
    {
        User_id = 176,
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
        var Order = this.Order;
        var result = _orderService.Create(Order);
        var Orders = _orderService.GetAll();
        
        Assert.Greater(Orders.Count, 0);
        
        _orderService.Delete(result);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var Order = this.Order;
        var result = _orderService.Create(Order);
        
        Assert.That(Order.User_id, Is.EqualTo(result.User_id));
        
        _orderService.Delete(result);
    }
    
    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var Order = this.Order;
        var createdOrder = _orderService.Create(Order);
        var result = _orderService.FindByID(createdOrder.Id);
       
        Assert.That(createdOrder.User_id, Is.EqualTo(result.User_id));
       
        _orderService.Delete(result);
    }

    [Test]
    public void OrderService_UpdateOrder_ReturnsTrue()
    {
        var Order = this.Order;
        var createdOrder = _orderService.Create(Order);
        
        Assert.That(createdOrder.User_id, Is.Not.EqualTo(239));
        createdOrder.User_id = 239;
        
        var result = _orderService.Update(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(_orderService.FindByID(createdOrder.Id).User_id, Is.EqualTo(239));
        
        _orderService.Delete(createdOrder);
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var Order = this.Order;
        var createdOrder = _orderService.Create(Order);
        var result = _orderService.Delete(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(_orderService.FindByID(createdOrder.Id).User_id, Is.EqualTo(0));
    }
}