using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_Database.Services.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class PaymentServiceTest
{
    private TikkiePaymentService _tikkiePaymentService = new();

    private string? _testTransaction;
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

        }
        else throw new Exception();
    }

    [Test]
    public void PaymentService_CreatingNewTransaction_ReturningLink()
    {
        _testTransaction = _tikkiePaymentService.CreatePaymentRequest(10000, 111111111, "TestingIfItReturnsALink");
        Assert.IsNotNull(_testTransaction);
        Assert.IsTrue(_testTransaction.Contains("https://sbx.tikkie.me/pay/"));
    }

    [Test]
    public void PaymentService_CreatingNewTransaction_ReturnsError()
    {
        Assert.IsTrue(_tikkiePaymentService.CreatePaymentRequest(0, 111111111, "TestingIfItReturnsALink")
            .Contains("errors"));
    }

    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsError()
    {
        Assert.IsTrue(_tikkiePaymentService.GetTransactionInformation("0").Contains("errors"));
    }
}