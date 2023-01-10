using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

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
        _roleService.Delete(result);

        Assert.Greater(Roles.Count, 0);
    }

    [Test]
    public void RoleService_CreateRole_ReturnsObject()
    {

        var Role = this.Role;
        var result = _roleService.Create(Role);
        _roleService.Delete(result);

        Assert.That(Role.Title, Is.EqualTo(result.Title));
    }
    
    [Test]
    public void RoleService_FindRole_ReturnsRole()
    {
        var Role = this.Role;
        var createdRole = _roleService.Create(Role);
        var result = _roleService.FindByID(createdRole.Id);
        _roleService.Delete(result);

        Assert.That(Role.Title, Is.EqualTo(result.Title));
    }

    [Test]
    public void RoleService_UpdateRole_ReturnsTrue()
    {
        var Role = this.Role;
        var createdRole = _roleService.Create(Role);
        
        Assert.That(createdRole.Title, Is.Not.EqualTo("Nieuwe titel!"));
        createdRole.Title = "Nieuwe titel!";
        
        var result = _roleService.Update(createdRole);
        string title = _roleService.FindByID(createdRole.Id).Title;
        _roleService.Delete(createdRole);

        Assert.IsTrue(result);
        Assert.That(title, Is.EqualTo("Nieuwe titel!"));
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