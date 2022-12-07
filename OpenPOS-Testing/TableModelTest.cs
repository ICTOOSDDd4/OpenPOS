using NUnit.Framework;
using OpenPOS_APP.Models;

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
        
        Assert.AreEqual(id, table.Id);
        Assert.AreEqual(Table_number, table.Table_number);
        Assert.AreEqual(Bill_id, table.Bill_id);
        Assert.AreEqual(Floor_id, table.Floor_id);
        
    }
}