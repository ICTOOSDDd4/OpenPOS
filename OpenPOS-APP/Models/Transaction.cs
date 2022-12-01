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



}