using NUnit.Framework;
using OpenPOS_APP.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class ProductModelTest
{
    [Test]
    public void Product_NewProduct_ReturnsProduct()
    {
        int id = 20;
        string name = "Test Product";
        double price = 10.00;
        string description = "Test Description";
        
        Product product = new Product(id,name, price, description);
        
        Assert.AreEqual(id, product.Id);
        Assert.AreEqual(name, product.Name);
        Assert.AreEqual(price, product.Price);
        Assert.AreEqual(description, product.Description);
    }
}