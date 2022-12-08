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
        Uri imagepath = new Uri("https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");


        Product product = new Product(id,name, price, description);
        
        Assert.AreEqual(id, product.Id);
        Assert.AreEqual(name, product.Name);
        Assert.AreEqual(price, product.Price);
        Assert.AreEqual(description, product.Description);
        Assert.AreEqual(imagepath, product.Imagepath);
    }
}