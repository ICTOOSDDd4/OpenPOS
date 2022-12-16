using Microsoft.Extensions.Configuration;
using OpenPOS_Models;
using OpenPOS_APP.Services;
using OpenPOS_Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class ProductServiceTest
{
    private ProductService _productService = new();

    private Product _product = new Product
    {
        Name = "Productnaam",
        Price = 55.25,
        Description = "Productbeschrijving",
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
    public void ProductService_GetAllProducts_ReturnsAllProducts()
    {
        var product = _product;
        var result = _productService.Create(product);
        var products = _productService.GetAll();
        _productService.Delete(result);
        
        Assert.Greater(products.Count, 0);
    }

    [Test]
    public void ProductService_CreateProduct_ReturnsObject()
    {

        var product = this._product;
        var result = _productService.Create(product);
        _productService.Delete(result);
        
        Assert.That(product.Name, Is.EqualTo(result.Name));
    }
    
    [Test]
    public void ProductService_FindProduct_ReturnsProduct()
    {
        var product = this._product;
        var createdProduct = _productService.Create(product);
        var result = _productService.FindByID(createdProduct.Id);
        _productService.Delete(result);
        
        Assert.That(product.Price, Is.EqualTo(result.Price));
    }

    [Test]
    public void ProductService_UpdateProduct_ReturnsTrue()
    {
        var product = this._product;
        var createdProduct = _productService.Create(product);
        
        Assert.That(createdProduct.Description, Is.Not.EqualTo("Nieuwe beschrijving!"));
        createdProduct.Description = "Nieuwe beschrijving!";
        
        var result = _productService.Update(createdProduct);
        _productService.Delete(createdProduct);
        
        Assert.IsTrue(result);
        Assert.That(_productService.FindByID(createdProduct.Id).Description, Is.EqualTo("Nieuwe beschrijving!"));
    }

    [Test]
    public void ProductService_DeleteProduct_ReturnsTrue()
    {
        var product = this._product;
        var createdProduct = _productService.Create(product);
        var result = _productService.Delete(createdProduct);
        
        Assert.IsTrue(result);
        Assert.That(_productService.FindByID(createdProduct.Id).Name, Is.Null);
    }
}