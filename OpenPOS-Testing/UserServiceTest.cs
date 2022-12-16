using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class UserServiceTest
{
    private UserService _userService = new();
    private UtilityService _utilityService = new();

    private User _user = new User
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
        var user = _user;
        var result = _userService.Create(user);
        var users = _userService.GetAll();
        _userService.Delete(result);

        Assert.Greater(users.Count, 0);
    }

    [Test]
    public void UserService_CreateUser_ReturnsObject()
    {

        var user = _user;
        var result = _userService.Create(user);
        _userService.Delete(result);

        Assert.That(user.Email, Is.EqualTo(result.Email));
    }
    
    [Test]
    public void UserService_FindUser_ReturnsUser()
    {
        var user = _user;
        var createdUser = _userService.Create(user);
        var result = _userService.FindByID(createdUser.Id);
        _userService.Delete(result);

        Assert.That(user.Email, Is.EqualTo(result.Email));
    }

    [Test]
    public void UserService_UpdateUser_ReturnsTrue()
    {
        var user = _user;
        var createdUser = _userService.Create(user);
        
        Assert.That(createdUser.Name, Is.Not.EqualTo("Gerard"));
        createdUser.Name = "Gerard";
        createdUser.Last_name = "Joling";
        
        var result = _userService.Update(createdUser);
        
        _userService.Delete(createdUser);

        
        Assert.IsTrue(result);
        Assert.That(_userService.FindByID(createdUser.Id).Name, Is.EqualTo("Gerard"));
    }

    [Test]
    public void UserService_DeleteUser_ReturnsTrue()
    {
        var user = _user;
        var createdUser = _userService.Create(user);
        var result = _userService.Delete(createdUser);
        
        Assert.IsTrue(result);
        Assert.IsNull(_userService.FindByID(createdUser.Id).Name);
    }

    [Test]
    public void UserService_LoginUser_ReturnsUserObject()
    {
        string email = "unittest@openpos.org";
        string password = "unittest";
        
        User user = _userService.Authenticate(email, _utilityService.HashPassword(password));
        
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