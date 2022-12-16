using Microsoft.Extensions.Configuration;
using OpenPOS_Database.ModelServices;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class CategoryServiceTest
{
    private CategoryService _categoryService = new();

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
        var result = _categoryService.Create(category);
        var categories = _categoryService.GetAll();
        _categoryService.Delete(result);

        Assert.Greater(categories.Count, 0);
    }

    [Test]
    public void CategoryService_CreateCategory_ReturnsObject()
    {

        var category = this._category;
        var result = _categoryService.Create(category);
        _categoryService.Delete(result);
        
        Assert.That(category.Name, Is.EqualTo(result.Name));
    }
    
    [Test]
    public void CategoryService_FindCategory_ReturnsCategory()
    {
        var category = this._category;
        var createdCategory = _categoryService.Create(category);
        var result = _categoryService.FindByID(createdCategory.Id);
        _categoryService.Delete(result);

        Assert.That(category.Name, Is.EqualTo(result.Name));
    }

    [Test]
    public void CategoryService_UpdateCategory_ReturnsTrue()
    {
        var category = this._category;
        var createdCategory = _categoryService.Create(category);
        
        Assert.That(createdCategory.Name, Is.Not.EqualTo("Sushi"));
        createdCategory.Name = "Sushi";
        
        var result = _categoryService.Update(createdCategory);
        _categoryService.Delete(createdCategory);

        Assert.IsTrue(result);
        Assert.That(_categoryService.FindByID(createdCategory.Id).Name, Is.EqualTo("Sushi"));
    }

    [Test]
    public void CategoryService_DeleteCategory_ReturnsTrue()
    {
        var category = this._category;
        var createdCategory = _categoryService.Create(category);
        var result = _categoryService.Delete(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(_categoryService.FindByID(createdCategory.Id).Name, Is.Null);
    }
}