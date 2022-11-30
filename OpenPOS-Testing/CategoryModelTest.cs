using NUnit.Framework;
using OpenPOS_APP.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class CategoryModelTest
{
    [Test]
    public void Category_NewCategory_ReturnsCategory()
    {
        int id = 1;
        string name = "Test Category";
        
        Category category = new Category(id, name);
        
        Assert.AreEqual(id, category.Id);
        Assert.AreEqual(name, category.Name);
    }
}