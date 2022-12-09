using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class ProductServiceTest
{
    private ProductService _productService = new();

    Product Product = new Product
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
        var Product = this.Product;
        var result = _productService.Create(Product);
        var Products = _productService.GetAll();
        
        Assert.Greater(Products.Count, 0);
        
        _productService.Delete(result);
    }

    [Test]
    public void ProductService_CreateProduct_ReturnsObject()
    {

        var Product = this.Product;
        var result = _productService.Create(Product);
        
        Assert.That(Product.Name, Is.EqualTo(result.Name));
        
        _productService.Delete(result);
    }
    
    [Test]
    public void ProductService_FindProduct_ReturnsProduct()
    {
        var Product = this.Product;
        var createdProduct = _productService.Create(Product);
        var result = _productService.FindByID(createdProduct.Id);
       
        Assert.That(Product.Price, Is.EqualTo(result.Price));
       
        _productService.Delete(result);
    }

    [Test]
    public void ProductService_UpdateProduct_ReturnsTrue()
    {
        var Product = this.Product;
        var createdProduct = _productService.Create(Product);
        
        Assert.That(createdProduct.Description, Is.Not.EqualTo("Nieuwe beschrijving!"));
        createdProduct.Description = "Nieuwe beschrijving!";
        
        var result = _productService.Update(createdProduct);
        
        Assert.IsTrue(result);
        Assert.That(_productService.FindByID(createdProduct.Id).Description, Is.EqualTo("Nieuwe beschrijving!"));
        
        _productService.Delete(createdProduct);
    }

    [Test]
    public void ProductService_DeleteProduct_ReturnsTrue()
    {
        var Product = this.Product;
        var createdProduct = _productService.Create(Product);
        var result = _productService.Delete(createdProduct);
        
        Assert.IsTrue(result);
        Assert.That(_productService.FindByID(createdProduct.Id).Name, Is.Null);
    }
}