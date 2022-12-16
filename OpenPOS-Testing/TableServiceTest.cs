using Microsoft.Extensions.Configuration;
using OpenPOS_Models;
using OpenPOS_APP.Services;
using OpenPOS_Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class TableServiceTest
{
    private TableService _tableService = new();

    private Table _table = new Table
    {
        Table_number = 69,
        Bill_id = 215,
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
        var table = _table;
        var result = _tableService.Create(table);
        var tables = _tableService.GetAll();
        _tableService.Delete(result);

        Assert.Greater(tables.Count, 0);
    }

    [Test]
    public void TableService_CreateTable_ReturnsObject()
    {
        var table = _table;
        var result = _tableService.Create(table);
        _tableService.Delete(result);

        Assert.That(result.Bill_id, Is.EqualTo(table.Bill_id));
    }
    
    [Test]
    public void TableService_FindTable_ReturnsTable()
    {
        var table = _table;
        var createdTable = _tableService.Create(table);
        var result = _tableService.FindByID(createdTable.Id);
        _tableService.Delete(result);

        Assert.That(table.Bill_id, Is.EqualTo(result.Bill_id));
    }

    [Test]
    public void TableService_UpdateTable_ReturnsTrue()
    {
        var table = _table;
        var createdTable = _tableService.Create(table);
        
        Assert.That(createdTable.Table_number, Is.Not.EqualTo(123));
        createdTable.Table_number = 123;
        
        var result = _tableService.Update(createdTable);
        _tableService.Delete(createdTable);

        Assert.IsTrue(result);
        Assert.That(_tableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(123));
    }

    [Test]
    public void TableService_DeleteTable_ReturnsTrue()
    {
        var table = _table;
        var createdTable = _tableService.Create(table);
        var result = _tableService.Delete(createdTable);
        
        Assert.IsTrue(result);
        Assert.That(_tableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(0));
    }

    [Test]
    public void TableService_FindByTableNumber_ReturnsTable()
    {
        var table = _table;
        var createdTable = _tableService.Create(table);
        var result = _tableService.FindByTableNumber(createdTable.Table_number);
        _tableService.Delete(createdTable);

        Assert.That(result.Table_number, Is.EqualTo(createdTable.Table_number));
    }
    
    [Test]
    public void TableService_FindByTableNumber_ReturnsNull()
    {
        var result = _tableService.FindByTableNumber(679);
        
        Assert.IsNull(result);

    }
}