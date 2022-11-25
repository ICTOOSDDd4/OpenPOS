using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class FloorServiceTest
{
    Floor Floor = new Floor
    {
        Storey = "6"
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
    public void FloorService_GetAllFloors_ReturnsAllFloors()
    {
        var Floor = this.Floor;
        var result = FloorService.Create(Floor);
        var Floors = FloorService.GetAll();
        
        Assert.Greater(Floors.Count, 0);
        
        FloorService.Delete(result);
    }

    [Test]
    public void FloorService_CreateFloor_ReturnsObject()
    {

        var Floor = this.Floor;
        var result = FloorService.Create(Floor);
        
        Assert.That(Floor.Storey, Is.EqualTo(result.Storey));
        
        FloorService.Delete(result);
    }
    
    [Test]
    public void FloorService_FindFloor_ReturnsFloor()
    {
        var Floor = this.Floor;
        var createdFloor = FloorService.Create(Floor);
        var result = FloorService.FindByID(createdFloor.Id);
       
        Assert.That(Floor.Storey, Is.EqualTo(result.Storey));
       
        FloorService.Delete(result);
    }

    [Test]
    public void FloorService_UpdateFloor_ReturnsTrue()
    {
        var Floor = this.Floor;
        var createdFloor = FloorService.Create(Floor);
        
        Assert.That(createdFloor.Storey, Is.Not.EqualTo("9"));
        createdFloor.Storey = "9";
        
        var result = FloorService.Update(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(FloorService.FindByID(createdFloor.Id).Storey, Is.EqualTo("9"));
        
        FloorService.Delete(createdFloor);
    }

    [Test]
    public void FloorService_DeleteFloor_ReturnsTrue()
    {
        var Floor = this.Floor;
        var createdFloor = FloorService.Create(Floor);
        var result = FloorService.Delete(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(FloorService.FindByID(createdFloor.Id).Storey, Is.Null);
    }
}