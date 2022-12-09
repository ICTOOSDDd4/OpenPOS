using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class RoleServiceTest
{
    private RoleService _roleService = new();

    Role Role = new Role
    {
        Title = "UnitTitel"
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
    public void RoleService_GetAllRoles_ReturnsAllRoles()
    {
        var Role = this.Role;
        var result = _roleService.Create(Role);
        var Roles = _roleService.GetAll();
        
        Assert.Greater(Roles.Count, 0);
        
        _roleService.Delete(result);
    }

    [Test]
    public void RoleService_CreateRole_ReturnsObject()
    {

        var Role = this.Role;
        var result = _roleService.Create(Role);
        
        Assert.That(Role.Title, Is.EqualTo(result.Title));
        
        _roleService.Delete(result);
    }
    
    [Test]
    public void RoleService_FindRole_ReturnsRole()
    {
        var Role = this.Role;
        var createdRole = _roleService.Create(Role);
        var result = _roleService.FindByID(createdRole.Id);
       
        Assert.That(Role.Title, Is.EqualTo(result.Title));
       
        _roleService.Delete(result);
    }

    [Test]
    public void RoleService_UpdateRole_ReturnsTrue()
    {
        var Role = this.Role;
        var createdRole = _roleService.Create(Role);
        
        Assert.That(createdRole.Title, Is.Not.EqualTo("Nieuwe titel!"));
        createdRole.Title = "Nieuwe titel!";
        
        var result = _roleService.Update(createdRole);
        
        Assert.IsTrue(result);
        Assert.That(_roleService.FindByID(createdRole.Id).Title, Is.EqualTo("Nieuwe titel!"));
        
        _roleService.Delete(createdRole);
    }

    [Test]
    public void RoleService_DeleteRole_ReturnsTrue()
    {
        var Role = this.Role;
        var createdRole = _roleService.Create(Role);
        var result = _roleService.Delete(createdRole);
        
        Assert.IsTrue(result);
        Assert.That(_roleService.FindByID(createdRole.Id).Title, Is.Null);
    }
}