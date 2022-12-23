using NUnit.Framework;
using OpenPOS_Models;

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
        string imagepath = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png";
        string allergies = "Test Allergies";

        Product product = new Product(id,name, price, description, imagepath, allergies);

        Assert.That(product.Id, Is.EqualTo(id));
        Assert.That(product.Name, Is.EqualTo(name));
        Assert.That(product.Price, Is.EqualTo(price));
        Assert.That(product.Description, Is.EqualTo(description));
        Assert.That(product.Imagepath, Is.EqualTo(imagepath));
        Assert.That(product.Allergies, Is.EqualTo(allergies));
   }
}