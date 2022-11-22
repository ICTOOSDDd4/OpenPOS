using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class CategoryServiceTest
{
    Category Category = new Category
    {
        Name = "UnitTest Category"
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
    public void CategoryService_GetAllCategorys_ReturnsAllCategorys()
    {
        var Category = this.Category;
        var result = CategoryService.Create(Category);
        var Categorys = CategoryService.GetAll();
        
        Assert.Greater(Categorys.Count, 0);
        
        CategoryService.Delete(result);
    }

    [Test]
    public void CategoryService_CreateCategory_ReturnsObject()
    {

        var Category = this.Category;
        var result = CategoryService.Create(Category);
        
        Assert.That(Category.Name, Is.EqualTo(result.Name));
        
        CategoryService.Delete(result);
    }
    
    [Test]
    public void CategoryService_FindCategory_ReturnsCategory()
    {
        var Category = this.Category;
        var createdCategory = CategoryService.Create(Category);
        var result = CategoryService.FindByID(createdCategory.Id);
       
        Assert.That(Category.Name, Is.EqualTo(result.Name));
       
        CategoryService.Delete(result);
    }

    [Test]
    public void CategoryService_UpdateCategory_ReturnsTrue()
    {
        var Category = this.Category;
        var createdCategory = CategoryService.Create(Category);
        
        Assert.That(createdCategory.Name, Is.Not.EqualTo("Sushi"));
        createdCategory.Name = "Sushi";
        
        var result = CategoryService.Update(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(CategoryService.FindByID(createdCategory.Id).Name, Is.EqualTo("Sushi"));
        
        CategoryService.Delete(createdCategory);
    }

    [Test]
    public void CategoryService_DeleteCategory_ReturnsTrue()
    {
        var Category = this.Category;
        var createdCategory = CategoryService.Create(Category);
        var result = CategoryService.Delete(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(CategoryService.FindByID(createdCategory.Id).Name, Is.Null);
    }
}