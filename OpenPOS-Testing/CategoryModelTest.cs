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
        
        Assert.That(category.Id, Is.EqualTo(id));
        Assert.That(category.Name, Is.EqualTo(name));
    }
}