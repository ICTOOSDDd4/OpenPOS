using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderModelTest
{
    [Test]
    public void Order_NewOrder_ReturnsOrder()
    {
        int id = 0;
        bool status = true;
        int userId = 20;
        int billId = 30;
        DateTime updatedAt = DateTime.Today;
        DateTime createdAt = DateTime.Now;
        
        Order order = new Order(id, status, userId, billId, updatedAt, createdAt);
        
        Assert.That(order.Id, Is.EqualTo(id));
        Assert.That(order.Status, Is.EqualTo(status));
        Assert.That(order.User_id, Is.EqualTo(userId));
        Assert.That(order.Bill_id, Is.EqualTo(billId));
        Assert.That(order.Updated_At, Is.EqualTo(updatedAt));
        Assert.That(order.Created_At, Is.EqualTo(createdAt));

    }
}