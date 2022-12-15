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
        Assert.AreEqual(firstName, user.Name);
        Assert.AreEqual(lastName, user.Last_name);
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(password, user.Password);
        
    }
}