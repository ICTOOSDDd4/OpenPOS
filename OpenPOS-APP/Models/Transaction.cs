namespace OpenPOS_APP.Models;

public class Transaction
{
    public string PaymentRequestToken { get; set; }
    public int AmountInCents { get; set; }
    public string TransactionId { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string Status { get; set; }
    public int NumberOfPayments { get; set; }
    public int TotalAmountPayed { get; set; }


   public Transaction() { }
   public Transaction(string PaymentRequestToken, int AmountInCents, string TransactionId, string Description, string Url, DateTime ExpiryDate, DateTime CreatedDateTime, string status, int numberOfPayments, int totalPayedAmount)
   {
      this.PaymentRequestToken = PaymentRequestToken;
      this.AmountInCents = AmountInCents;
      this.TransactionId = TransactionId;
      this.Description = Description;
      this.Url = Url;
      this.ExpiryDate = ExpiryDate;
      this.CreatedDateTime = CreatedDateTime;
      this.Status = status;
      this.NumberOfPayments = numberOfPayments;
      this.TotalAmountPayed = totalPayedAmount;
   }
}