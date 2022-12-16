using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class RoleModelTest
{
    [Test]
    public void Role_NewRole_ReturnsRole()
    {
        int id = 1;
        string title = "Admin";

        Role role = new Role(id, title);
        
        Assert.That(role.Id, Is.EqualTo(id));
        Assert.That(role.Title, Is.EqualTo(title));

    }
}