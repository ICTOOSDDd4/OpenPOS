using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class UserModelTest
{
    [Test]
    public void User_NewUser_ReturnsUser()
    {
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@email.com";
        string password = "password";
        User user = new User(firstName,lastName,email,password);
        Assert.That(user.Name, Is.EqualTo(firstName));
        Assert.That(user.Last_name, Is.EqualTo(lastName));
        Assert.That(user.Email, Is.EqualTo(email));
        Assert.That(user.Password, Is.EqualTo(password));
        
    }
}