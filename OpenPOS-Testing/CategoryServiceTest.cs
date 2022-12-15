using Microsoft.Extensions.Configuration;
using OpenPOS_Database.ModelServices;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class CategoryServiceTest
{
    private CategoryService _categoryService = new();

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
        var result = _categoryService.Create(Category);
        var Categorys = _categoryService.GetAll();
        
        Assert.Greater(Categorys.Count, 0);

        _categoryService.Delete(result);
    }

    [Test]
    public void CategoryService_CreateCategory_ReturnsObject()
    {

        var Category = this.Category;
        var result = _categoryService.Create(Category);
        
        Assert.That(Category.Name, Is.EqualTo(result.Name));

        _categoryService.Delete(result);
    }
    
    [Test]
    public void CategoryService_FindCategory_ReturnsCategory()
    {
        var Category = this.Category;
        var createdCategory = _categoryService.Create(Category);
        var result = _categoryService.FindByID(createdCategory.Id);
       
        Assert.That(Category.Name, Is.EqualTo(result.Name));

        _categoryService.Delete(result);
    }

    [Test]
    public void CategoryService_UpdateCategory_ReturnsTrue()
    {
        var Category = this.Category;
        var createdCategory = _categoryService.Create(Category);
        
        Assert.That(createdCategory.Name, Is.Not.EqualTo("Sushi"));
        createdCategory.Name = "Sushi";
        
        var result = _categoryService.Update(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(_categoryService.FindByID(createdCategory.Id).Name, Is.EqualTo("Sushi"));

        _categoryService.Delete(createdCategory);
    }

    [Test]
    public void CategoryService_DeleteCategory_ReturnsTrue()
    {
        var Category = this.Category;
        var createdCategory = _categoryService.Create(Category);
        var result = _categoryService.Delete(createdCategory);
        
        Assert.IsTrue(result);
        Assert.That(_categoryService.FindByID(createdCategory.Id).Name, Is.Null);
    }
}