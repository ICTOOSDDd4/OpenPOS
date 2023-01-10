using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderLineModelTest
{
    [Test]
    public void OrderLine_NewOrderLine_ReturnsOrderLine()
    {
        int orderId = 1;
        int productId = 3;
        int amount = 5;
        string comment = "Test";
        
        OrderLine orderLine = new OrderLine(orderId, productId, amount, comment);
        
        Assert.That(orderLine.Order_id, Is.EqualTo(orderId));
        Assert.That(orderLine.Product_id, Is.EqualTo(productId));
        Assert.That(orderLine.Amount, Is.EqualTo(amount));
        Assert.That(orderLine.Comment, Is.EqualTo(comment));
        
    }
}