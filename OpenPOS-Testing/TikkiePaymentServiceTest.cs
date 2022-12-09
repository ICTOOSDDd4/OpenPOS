using System.Reflection;
using Microsoft.Extensions.Configuration;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using OpenPOS_Settings;

namespace OpenPOS_Testing;

[TestFixture]
public class PaymentServiceTest
{
    private TikkiePaymentService _tikkiePaymentService = new();

    private Transaction? _testTransaction;
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
        Assert.IsNotEmpty(_testTransaction.Url);
        Assert.IsNotEmpty(_testTransaction.PaymentRequestToken);
    }
    
    [Test]
    public void PaymentService_CreatingNewTransaction_ReturnsError()
    {
        Assert.Throws<Exception>(delegate { _tikkiePaymentService.CreatePaymentRequest(0, 111111111, "TestingIfItReturnsALink"); });
    }
    
    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsError()
    {
        Assert.Throws<Exception>(delegate { _tikkiePaymentService.GetTransactionInformation("0"); } );
    }
    
    [Test]
    public void PaymentService_FetchingTransactionInfo_ReturnsTransaction()
    {
        if (_testTransaction != null)
        {
            Transaction transaction = _tikkiePaymentService.GetTransactionInformation(_testTransaction.PaymentRequestToken);
            Assert.That(_testTransaction.Url, Is.EqualTo(transaction.Url));
            Assert.That(_testTransaction.AmountInCents, Is.EqualTo(transaction.AmountInCents));
            Assert.That(_testTransaction.ExpiryDate, Is.EqualTo(transaction.ExpiryDate));
            Assert.That(_testTransaction.PaymentRequestToken, Is.EqualTo(transaction.PaymentRequestToken));
        }
    }
}