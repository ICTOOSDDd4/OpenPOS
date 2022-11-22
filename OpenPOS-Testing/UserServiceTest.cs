using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_Testing;

[TestFixture]
public class UserServiceTest
{
    User user = new User
    {
        Name = "Voornaam",
        Last_name = "Achternaam",
        Email = "email",
        Password = "TestPassword"
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
    public void UserService_GetAllUsers_ReturnsAllUsers()
    {
        var user = this.user;

        var result = UserService.Create(user);
        var users = UserService.GetAll();
        Assert.Greater(users.Count, 0);
        UserService.Delete(result);
    }

    [Test]
    public void UserService_CreateUser_ReturnsObject()
    {

        var user = this.user;

        var result = UserService.Create(user);
        
        Assert.That(user.Email, Is.EqualTo(result.Email));
        UserService.Delete(result);
    }
    
    [Test]
    public void UserService_FindUser_ReturnsUser()
    {
        var user = this.user;
        var createdUser = UserService.Create(user);
        var result = UserService.FindByID(createdUser.Id);
        Assert.That(user.Email, Is.EqualTo(result.Email));
        
        UserService.Delete(result);
    }

    [Test]
    public void UserService_UpdateUser_ReturnsTrue()
    {
        var user = this.user;
        
        var createdUser = UserService.Create(user);
        Assert.That(createdUser.Name, Is.Not.EqualTo("Gerard"));
        createdUser.Name = "Gerard";
        createdUser.Last_name = "Joling";
        var result = UserService.Update(createdUser);
        Assert.IsTrue(result);
        Assert.That(UserService.FindByID(createdUser.Id).Name, Is.EqualTo("Gerard"));
        
        UserService.Delete(createdUser);
    }

    [Test]
    public void UserService_DeleteUser_ReturnsTrue()
    {
        var user = this.user;
        
        var createdUser = UserService.Create(user);
        var result = UserService.Delete(createdUser);
        Assert.IsTrue(result);
        Assert.IsNull(UserService.FindByID(createdUser.Id).Name);
    }

    [Test]
    public void UserService_LoginUser_ReturnsUserObject()
    {
        //Deze user is al aangemaakt in de database
        var email = "unittest@openpos.org";
        var password = "unittest";
        var user = UserService.Authenticate(email, password);
        
        Assert.That(user.Email, Is.EqualTo(email));
    }
    
    
    [Test]
    public void UserService_LoginUser_ReturnsNull()
    {
        string email = "unittest@openpos.org";
        string password = "wrongpassword";
        User user = UserService.Authenticate(email, password);
        
        Assert.IsNull(user);
    }
}