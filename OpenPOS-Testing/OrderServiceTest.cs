using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderServiceTest
{
    Order Order = new Order
    {
        User_id = 176,
        Bill_id = 3,
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
        var result = OrderService.Create(Order);
        var Orders = OrderService.GetAll();
        
        Assert.Greater(Orders.Count, 0);
        
        OrderService.Delete(result);
    }

    [Test]
    public void OrderService_CreateOrder_ReturnsObject()
    {

        var Order = this.Order;
        var result = OrderService.Create(Order);
        
        Assert.That(Order.User_id, Is.EqualTo(result.User_id));
        
        OrderService.Delete(result);
    }
    
    [Test]
    public void OrderService_FindOrder_ReturnsOrder()
    {
        var Order = this.Order;
        var createdOrder = OrderService.Create(Order);
        var result = OrderService.FindByID(createdOrder.Id);
       
        Assert.That(createdOrder.User_id, Is.EqualTo(result.User_id));
       
        OrderService.Delete(result);
    }

    [Test]
    public void OrderService_UpdateOrder_ReturnsTrue()
    {
        var Order = this.Order;
        var createdOrder = OrderService.Create(Order);
        
        Assert.That(createdOrder.User_id, Is.Not.EqualTo(239));
        createdOrder.User_id = 239;
        
        var result = OrderService.Update(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(OrderService.FindByID(createdOrder.Id).User_id, Is.EqualTo(239));
        
        OrderService.Delete(createdOrder);
    }

    [Test]
    public void OrderService_DeleteOrder_ReturnsTrue()
    {
        var Order = this.Order;
        var createdOrder = OrderService.Create(Order);
        var result = OrderService.Delete(createdOrder);
        
        Assert.IsTrue(result);
        Assert.That(OrderService.FindByID(createdOrder.Id).User_id, Is.EqualTo(0));
    }
}