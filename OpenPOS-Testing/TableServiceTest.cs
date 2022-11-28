using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class TableServiceTest
{
    Table Table = new Table
    {
        Table_number = 69,
        Bill_id = 3,
        Floor_id = 2
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
    public void TableService_GetAllTables_ReturnsAllTables()
    {
        var Table = this.Table;
        var result = TableService.Create(Table);
        var Tables = TableService.GetAll();
        
        Assert.Greater(Tables.Count, 0);
        
        TableService.Delete(result);
    }

    [Test]
    public void TableService_CreateTable_ReturnsObject()
    {

        var Table = this.Table;
        var result = TableService.Create(Table);

        Assert.That(result.Bill_id, Is.EqualTo(Table.Bill_id));
        
        TableService.Delete(result);
    }
    
    [Test]
    public void TableService_FindTable_ReturnsTable()
    {
        var Table = this.Table;
        var createdTable = TableService.Create(Table);
        var result = TableService.FindByID(createdTable.Id);
       
        Assert.That(Table.Bill_id, Is.EqualTo(result.Bill_id));
       
        TableService.Delete(result);
    }

    [Test]
    public void TableService_UpdateTable_ReturnsTrue()
    {
        var Table = this.Table;
        var createdTable = TableService.Create(Table);
        
        Assert.That(createdTable.Table_number, Is.Not.EqualTo(123));
        createdTable.Table_number = 123;
        
        var result = TableService.Update(createdTable);
        
        Assert.IsTrue(result);
        Assert.That(TableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(123));
        
        TableService.Delete(createdTable);
    }

    [Test]
    public void TableService_DeleteTable_ReturnsTrue()
    {
        var Table = this.Table;
        var createdTable = TableService.Create(Table);
        var result = TableService.Delete(createdTable);
        
        Assert.IsTrue(result);
        Assert.That(TableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(0));
    }

    [Test]
    public void TableService_FindByTableNumber_ReturnsTable()
    {
        var Table = this.Table;
        var createdTable = TableService.Create(Table);
        var result = TableService.FindByTableNumber(createdTable.Table_number);
        
        Assert.That(result.Table_number, Is.EqualTo(createdTable.Table_number));

        TableService.Delete(createdTable);

    }
}