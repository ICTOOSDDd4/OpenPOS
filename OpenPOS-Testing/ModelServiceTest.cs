using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_Testing;

[TestFixture]
public class ModelServiceTest
{
    public static IConfiguration InitConfiguration()
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("OpenPOS_Testing.appsettings.test.json");
        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            
            return config;
        }

        return null;
    }
    
    [SetUp]
    public void SetUp()
    {
        var config = InitConfiguration();
        DatabaseService.Initialize(config["DATABASE_CONNECTION.connection_string"]);
    }

    [Test]
    public void UserService_GetAllUsers_ReturnsAllUsers()
    {
        var users = UserService.GetAll();
        Assert.Greater(users.Count, 1);
    }
    
    
    [Test]
    public void UserService_FindUser_ReturnsUser()
    {
        var user = UserService.FindByID(1);
        Assert.AreEqual(user.Id, 1);
    }
    
    [Test]
    public void UserService_FindUser_ReturnsNull()
    {
        var user = UserService.FindByID(0);
        Assert.IsNull(user);
    }

    [Test]
    public void UserService_CreateUser_ReturnsTrue()
    {
        var user = new User
        {
            Name = "Voornaam",
            Last_name = "Achternaam",
            Email = "email",
            Password = "TestPassword"
        };
        var result = UserService.Create(user);
        Assert.IsTrue(result);
    }

    [Test]
    public void UserService_DeleteUser_ReturnsTrue()
    {
        var user = UserService.FindByID(2);
        var result = UserService.Delete(user);
        Assert.IsTrue(result);
    }

    [Test]
    public void UserService_UpdateUser_ReturnsTrue()
    {
        var user = UserService.FindByID(1);
        user.Name = "Gerard";
        user.Last_name = "Joling";
        var result = UserService.Update(user);
        Assert.IsTrue(result);
    }
    
    
    
    
}