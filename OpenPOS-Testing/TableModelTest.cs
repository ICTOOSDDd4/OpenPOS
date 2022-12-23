using NUnit.Framework;
using OpenPOS_Models;

namespace OpenPOS_Testing;

[TestFixture]
public class TableModelTest
{
    [Test]
    public void Table_NewTable_ReturnsTable()
    {
        int id = 2;
        int Table_number = 5;
        int Bill_id = 0;
        int Floor_id = 2;
        
        Table table = new Table(id, Table_number, Bill_id, Floor_id);
        
        Assert.That(table.Id, Is.EqualTo(id));
        Assert.That(table.Table_number, Is.EqualTo(Table_number));
        Assert.That(table.Bill_id, Is.EqualTo(Bill_id));
        Assert.That(table.Floor_id, Is.EqualTo(Floor_id));
        
    }
}