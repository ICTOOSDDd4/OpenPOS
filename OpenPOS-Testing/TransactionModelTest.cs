using NUnit.Framework.Internal;
using OpenPOS_APP.Models;

namespace OpenPOS_Testing;

[TestFixture]
public class TransactionModelTest
{       
    [Test]
    public void Transaction_NewTransaction_ReturnsTransactionModel()
    {
      string PaymentRequestToken = "jkdsfsfjs";
      int AmountInCents = 2334;
      string TransactionId = "d3432";
      string Description = "This is a very good desc";
      string Url = "https://google.com";
      DateTime ExpiryDate = DateTime.Now;
      DateTime CreatedDateTime = DateTime.Now;
      string status = "PAYED";
      int numberOfPayments = 2;
      int totalPayedAmount = 0;


      Transaction trans = new Transaction(PaymentRequestToken, AmountInCents, TransactionId, Description, Url, ExpiryDate, CreatedDateTime, status, numberOfPayments, totalPayedAmount);

      Assert.That(trans.PaymentRequestToken, Is.EqualTo(PaymentRequestToken));
      Assert.That(trans.AmountInCents, Is.EqualTo(AmountInCents));
      Assert.That(trans.TransactionId, Is.EqualTo(TransactionId));
      Assert.That(trans.Description, Is.EqualTo(Description));
      Assert.That(trans.Url, Is.EqualTo(Url));
      Assert.That(trans.ExpiryDate, Is.EqualTo(ExpiryDate));
      Assert.That(trans.CreatedDateTime, Is.EqualTo(CreatedDateTime));
      Assert.That(trans.Status, Is.EqualTo(status));
      Assert.That(trans.NumberOfPayments, Is.EqualTo(numberOfPayments));
      Assert.That(trans.TotalAmountPayed, Is.EqualTo(totalPayedAmount));

    }
}