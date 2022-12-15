using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing;

[TestFixture]
public class FloorServiceTest
{
    private FloorService _floorService = new();

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
        var result = _floorService.Create(Floor);
        var Floors = _floorService.GetAll();
        
        Assert.Greater(Floors.Count, 0);

        _floorService.Delete(result);
    }

    [Test]
    public void FloorService_CreateFloor_ReturnsObject()
    {

        var Floor = this.Floor;
        var result = _floorService.Create(Floor);
        
        Assert.That(Floor.Storey, Is.EqualTo(result.Storey));
        
        _floorService.Delete(result);
    }
    
    [Test]
    public void FloorService_FindFloor_ReturnsFloor()
    {
        var Floor = this.Floor;
        var createdFloor = _floorService.Create(Floor);
        var result = _floorService.FindByID(createdFloor.Id);
       
        Assert.That(Floor.Storey, Is.EqualTo(result.Storey));
       
        _floorService.Delete(result);
    }

    [Test]
    public void FloorService_UpdateFloor_ReturnsTrue()
    {
        var Floor = this.Floor;
        var createdFloor = _floorService.Create(Floor);
        
        Assert.That(createdFloor.Storey, Is.Not.EqualTo("9"));
        createdFloor.Storey = "9";
        
        var result = _floorService.Update(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(_floorService.FindByID(createdFloor.Id).Storey, Is.EqualTo("9"));
        
        _floorService.Delete(createdFloor);
    }

    [Test]
    public void FloorService_DeleteFloor_ReturnsTrue()
    {
        var Floor = this.Floor;
        var createdFloor = _floorService.Create(Floor);
        var result = _floorService.Delete(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(_floorService.FindByID(createdFloor.Id).Storey, Is.Null);
    }
}