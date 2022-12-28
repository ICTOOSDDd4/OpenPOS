using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class OrderLineModelTest
{
    [Test]
    public void OrderLine_NewOrderLine_ReturnsOrderLine()
    {
        int Order_Id = 1;
        int Product_Id = 3;
        int Amount = 5;
        string Comment = "Test";
        
        OrderLine orderLine = new OrderLine(Order_Id, Product_Id, Amount, Comment);
        
        Assert.That(orderLine.Order_id, Is.EqualTo(Order_Id));
        Assert.That(orderLine.Product_id, Is.EqualTo(Product_Id));
        Assert.That(orderLine.Amount, Is.EqualTo(Amount));
        Assert.That(orderLine.Comment, Is.EqualTo(Comment));
        
    }
}