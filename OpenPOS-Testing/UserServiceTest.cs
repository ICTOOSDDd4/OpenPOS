using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;

namespace OpenPOS_Testing;

[TestFixture]
public class UserServiceTest
{
    private UserService _userService = new();

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
        var result = _userService.Create(user);
        var users = _userService.GetAll();
        
        Assert.Greater(users.Count, 0);
        
        _userService.Delete(result);
    }

    [Test]
    public void UserService_CreateUser_ReturnsObject()
    {

        var user = this.user;
        var result = _userService.Create(user);
        
        Assert.That(user.Email, Is.EqualTo(result.Email));
       
        _userService.Delete(result);
    }
    
    [Test]
    public void UserService_FindUser_ReturnsUser()
    {
        var user = this.user;
        var createdUser = _userService.Create(user);
        var result = _userService.FindByID(createdUser.Id);
        
        Assert.That(user.Email, Is.EqualTo(result.Email));
        
        _userService.Delete(result);
    }

    [Test]
    public void UserService_UpdateUser_ReturnsTrue()
    {
        var user = this.user;
        var createdUser = _userService.Create(user);
        
        Assert.That(createdUser.Name, Is.Not.EqualTo("Gerard"));
        createdUser.Name = "Gerard";
        createdUser.Last_name = "Joling";
        
        var result = _userService.Update(createdUser);
        
        Assert.IsTrue(result);
        Assert.That(_userService.FindByID(createdUser.Id).Name, Is.EqualTo("Gerard"));
        
        _userService.Delete(createdUser);
    }

    [Test]
    public void UserService_DeleteUser_ReturnsTrue()
    {
        var user = this.user;
        var createdUser = _userService.Create(user);
        var result = _userService.Delete(createdUser);
        
        Assert.IsTrue(result);
        Assert.IsNull(_userService.FindByID(createdUser.Id).Name);
    }

    [Test]
    public void UserService_LoginUser_ReturnsUserObject()
    {
        //Deze user is al aangemaakt in de database
        string email = "unittest@openpos.org";
        string password = "unittest";
        
        User user = _userService.Authenticate(email, password);
        
        Assert.That(user.Email, Is.EqualTo(email));
    }
    
    
    [Test]
    public void UserService_LoginUser_ReturnsNull()
    {
        string email = "unittest@openpos.org";
        string password = "wrongpassword";
       
        User user = _userService.Authenticate(email, password);
        
        Assert.IsNull(user);
    }
}