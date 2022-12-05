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
        bool Status = false;
        int Amount = 5;
        string Comment = "Test";
        
        OrderLine orderLine = new OrderLine(Order_Id, Product_Id, Status, Amount, Comment);
        
        Assert.AreEqual(Order_Id, orderLine.Order_id);
        Assert.AreEqual(Product_Id, orderLine.Product_id);
        Assert.AreEqual(Status, orderLine.Status);
        Assert.AreEqual(Amount, orderLine.Amount);
        Assert.AreEqual(Comment, orderLine.Comment);
        
    }
}