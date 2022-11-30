using NUnit.Framework;
using OpenPOS_APP.Models;

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
        
        Assert.AreEqual(Order_Id, orderLine.Order_id);
        Assert.AreEqual(Product_Id, orderLine.Product_id);
        Assert.AreEqual(Amount, orderLine.Amount);
        Assert.AreEqual(Comment, orderLine.Comment);
        
    }
}