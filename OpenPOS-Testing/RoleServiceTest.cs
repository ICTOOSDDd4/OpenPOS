using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class RoleServiceTest
{
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
        var result = RoleService.Create(Role);
        var Roles = RoleService.GetAll();
        
        Assert.Greater(Roles.Count, 0);
        
        RoleService.Delete(result);
    }

    [Test]
    public void RoleService_CreateRole_ReturnsObject()
    {

        var Role = this.Role;
        var result = RoleService.Create(Role);
        
        Assert.That(Role.Title, Is.EqualTo(result.Title));
        
        RoleService.Delete(result);
    }
    
    [Test]
    public void RoleService_FindRole_ReturnsRole()
    {
        var Role = this.Role;
        var createdRole = RoleService.Create(Role);
        var result = RoleService.FindByID(createdRole.Id);
       
        Assert.That(Role.Title, Is.EqualTo(result.Title));
       
        RoleService.Delete(result);
    }

    [Test]
    public void RoleService_UpdateRole_ReturnsTrue()
    {
        var Role = this.Role;
        var createdRole = RoleService.Create(Role);
        
        Assert.That(createdRole.Title, Is.Not.EqualTo("Nieuwe titel!"));
        createdRole.Title = "Nieuwe titel!";
        
        var result = RoleService.Update(createdRole);
        
        Assert.IsTrue(result);
        Assert.That(RoleService.FindByID(createdRole.Id).Title, Is.EqualTo("Nieuwe titel!"));
        
        RoleService.Delete(createdRole);
    }

    [Test]
    public void RoleService_DeleteRole_ReturnsTrue()
    {
        var Role = this.Role;
        var createdRole = RoleService.Create(Role);
        var result = RoleService.Delete(createdRole);
        
        Assert.IsTrue(result);
        Assert.That(RoleService.FindByID(createdRole.Id).Title, Is.Null);
    }
}