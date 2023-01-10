using Microsoft.Extensions.Configuration;
using System.Reflection;
using OpenPOS_Database.ModelServices;

namespace OpenPOS_Testing;

[TestFixture]
public class FloorServiceTest
{
    private FloorService _floorService = new();

    private Floor _floor = new Floor
    {
        Storey = "7"
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
        var floor = _floor;
        var result = _floorService.Create(floor);
        var floors = _floorService.GetAll();
        _floorService.Delete(result);

        Assert.Greater(floors.Count, 0);
    }

    [Test]
    public void FloorService_CreateFloor_ReturnsObject()
    {

        var floor = _floor;
        var result = _floorService.Create(floor);
        _floorService.Delete(result);
        
        Assert.That(floor.Storey, Is.EqualTo(result.Storey));
    }
    
    [Test]
    public void FloorService_FindFloor_ReturnsFloor()
    {
        var floor = _floor;
        var createdFloor = _floorService.Create(floor);
        var result = _floorService.FindByID(createdFloor.Id);
        _floorService.Delete(result);
        
        Assert.That(floor.Storey, Is.EqualTo(result.Storey));
    }

    [Test]
    public void FloorService_UpdateFloor_ReturnsTrue()
    {
        var floor =_floor;
        var createdFloor = _floorService.Create(floor);
        
        Assert.That(createdFloor.Storey, Is.Not.EqualTo("9"));
        createdFloor.Storey = "9";
        
        var result = _floorService.Update(createdFloor);
        string re = _floorService.FindByID(createdFloor.Id).Storey;
        _floorService.Delete(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(re, Is.EqualTo("9"));
    }

    [Test]
    public void FloorService_DeleteFloor_ReturnsTrue()
    {
        var floor = _floor;
        var createdFloor = _floorService.Create(floor);
        var result = _floorService.Delete(createdFloor);
        
        Assert.IsTrue(result);
        Assert.That(_floorService.FindByID(createdFloor.Id).Storey, Is.Null);
    }
}