using Microsoft.Extensions.Configuration;
using OpenPOS_Controllers;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class CategoryControllerTest
{
    private CategoryController _categoryController = new();

    private Category _category = new Category
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
        var category = _category;
        var result = _categoryController.CreateNew(category.Name);
        var categories = _categoryController.GetAll();
        _categoryController.Delete(result);

        Assert.Greater(categories.Count, 0);
    }

    [Test]
    public void CategoryService_CreateCategory_ReturnsObject()
    {

        var category = this._category;
        var result = _categoryController.CreateNew(category.Name);
        _categoryController.Delete(result);
        
        Assert.That(category.Name, Is.EqualTo(result.Name));
    }
    
    [Test]
    public void CategoryService_FindCategory_ReturnsCategory()
    {
        var category = this._category;
        var createdCategory = _categoryController.CreateNew(category.Name);
        var result = _categoryController.Get(createdCategory.Id);
        _categoryController.Delete(result);

        Assert.That(category.Name, Is.EqualTo(result.Name));
    }

    [Test]
    public void CategoryService_UpdateCategory_ReturnsTrue()
    {
        var category = this._category;
        var createdCategory = _categoryController.CreateNew(category.Name);
        
        Assert.That(createdCategory.Name, Is.Not.EqualTo("Sushi"));
        createdCategory.Name = "Sushi";
        
        var result = _categoryController.UpdateAll(createdCategory);
        string re = _categoryController.Get(createdCategory.Id).Name;
        _categoryController.Delete(createdCategory);

        Assert.IsTrue(result);
        Assert.That(re, Is.EqualTo("Sushi"));
    }

    [Test]
    public void CategoryService_DeleteCategory_ReturnsTrue()
    {
        var category = this._category;
        var createdCategory = _categoryController.CreateNew(category.Name);
        var result = _categoryController.Delete(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(_categoryController.Get(createdCategory.Id).Name, Is.Null);
    }
}