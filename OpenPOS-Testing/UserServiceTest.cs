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
    private User _user;
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
        var users = UserService.GetAll();
        Assert.Greater(users.Count, 1);
    }

    [Test]
    public void UserService_CreateUser_ReturnsObject()
    {

        var user = new User
        {
            Name = "Voornaam",
            Last_name = "Achternaam",
            Email = "email",
            Password = "TestPassword"
        };
        _user = user;
        var result = UserService.Create(user);
        Assert.AreEqual(user.Email, result.Email);
    }
    
    [Test]
    public void UserService_FindUser_ReturnsUser()
    {
        UserService_CreateUser_ReturnsObject();
        var user = UserService.FindByID(_user.Id);
        Assert.AreEqual(_user.Email, user.Email);
        UserService_DeleteUser_ReturnsTrue();
    }

    [Test]
    public void UserService_UpdateUser_ReturnsTrue()
    {
        var user = UserService.FindByID(_user.Id);
        user.Name = "Gerard";
        user.Last_name = "Joling";
        var result = UserService.Update(user);
        Assert.IsTrue(result);
    }

    [Test]
    public void UserService_DeleteUser_ReturnsTrue()
    {
        var user = UserService.FindByID(_user.Id);
        var result = UserService.Delete(user);
        Assert.IsTrue(result);
    }
}