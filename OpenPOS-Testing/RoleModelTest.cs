using NUnit.Framework;
using OpenPOS_APP.Models;

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
        
        Assert.AreEqual(id, role.Id);
        Assert.AreEqual(title, role.Title);

    }
}