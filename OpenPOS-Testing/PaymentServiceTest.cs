using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_Testing;

public class PaymentServiceTest
{
    [SetUp]
    public void Setup()
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("OpenPOS_Testing.appsettings.test.json");

        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            
            ApplicationSettings.DbSett = config.GetRequiredSection("DATABASE_CONNECTION").Get<DatabaseSettings>();
            ApplicationSettings.TikkieSet = config.GetRequiredSection("TIKKIE_API").Get<TikkieSettings>();
            if (ApplicationSettings.TikkieSet != null)
            {
                PaymentService.PayMethod = new PaymentMethod();
            }
        }
        else throw new Exception();
    }

    [Test]
    public void PaymentService_CreatingNewTransaction_ReturningLink()
    {
        
    }
    
    [Test]
    public void PaymentService_CreatingNewTransaction_ReturnsError()
    {
        
    }
    
    [Test]
    public void PaymentService_CreatingNewTransaction_ReturnsNull()
    {
        
    }
    
    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsNull()
    {
        
    }
    
    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsError()
    {
        
    }
    
    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsTransaction()
    {
        
    }
}