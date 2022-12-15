using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class FloorModelTest
{
    [Test]
    public void Floor_NewFloor_ReturnsFloor()
    {
        int id = 2;
        string storey = "Very High";
        
        Floor floor = new Floor(id, storey);
        
        Assert.AreEqual(id, floor.Id);
        Assert.AreEqual(storey, floor.Storey);
    }
}