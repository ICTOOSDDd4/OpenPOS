using NUnit.Framework.Internal;
using OpenPOS_APP.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class TransactionModelTest
{
    private Transaction t1 = new Transaction()
    {
        PaymentRequestToken = "jkdsfsfjs",
        AmountInCents = 2334,
        TransactionId = "d3432",
        Description = "This is a very good desc",
        Url = "https://google.com",
    };
        
    [Test]
    public void Transaction_NewTransaction_ReturnsTransactionModel()
    {
        
    }
}