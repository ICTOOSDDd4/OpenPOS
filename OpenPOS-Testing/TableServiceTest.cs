using Microsoft.Extensions.Configuration;
using OpenPOS_Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_Database;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;
using System.Reflection;

namespace OpenPOS_Testing;

[TestFixture]
public class TableServiceTest
{
    private TableService _tableService = new();
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
        var result = _tableService.Create(Table);
        var Tables = _tableService.GetAll();
        
        Assert.Greater(Tables.Count, 0);
        
        _tableService.Delete(result);
    }

    [Test]
    public void TableService_CreateTable_ReturnsObject()
    {

        var Table = this.Table;
        var result = _tableService.Create(Table);

        Assert.That(result.Bill_id, Is.EqualTo(Table.Bill_id));
        
        _tableService.Delete(result);
    }
    
    [Test]
    public void TableService_FindTable_ReturnsTable()
    {
        var Table = this.Table;
        var createdTable = _tableService.Create(Table);
        var result = _tableService.FindByID(createdTable.Id);
       
        Assert.That(Table.Bill_id, Is.EqualTo(result.Bill_id));
       
        _tableService.Delete(result);
    }

    [Test]
    public void TableService_UpdateTable_ReturnsTrue()
    {
        var Table = this.Table;
        var createdTable = _tableService.Create(Table);
        
        Assert.That(createdTable.Table_number, Is.Not.EqualTo(123));
        createdTable.Table_number = 123;
        
        var result = _tableService.Update(createdTable);
        
        Assert.IsTrue(result);
        Assert.That(_tableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(123));
        
        _tableService.Delete(createdTable);
    }

    [Test]
    public void TableService_DeleteTable_ReturnsTrue()
    {
        var Table = this.Table;
        var createdTable = _tableService.Create(Table);
        var result = _tableService.Delete(createdTable);
        
        Assert.IsTrue(result);
        Assert.That(_tableService.FindByID(createdTable.Id).Table_number, Is.EqualTo(0));
    }

    [Test]
    public void TableService_FindByTableNumber_ReturnsTable()
    {
        var Table = this.Table;
        var createdTable = _tableService.Create(Table);
        var result = _tableService.FindByTableNumber(createdTable.Table_number);
        
        Assert.That(result.Table_number, Is.EqualTo(createdTable.Table_number));

        _tableService.Delete(createdTable);

    }
    
    [Test]
    public void TableService_FindByTableNumber_ReturnsNull()
    {
        var result = _tableService.FindByTableNumber(679);
        
        Assert.IsNull(result);

    }
}