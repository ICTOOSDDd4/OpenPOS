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
        
        Assert.AreEqual(id, order.Id);
        Assert.AreEqual(status, order.Status);
        Assert.AreEqual(userId, order.User_id);
        Assert.AreEqual(billId, order.Bill_id);
        Assert.AreEqual(updatedAt, order.Updated_At);
        Assert.AreEqual(createdAt, order.Created_At);

    }
}