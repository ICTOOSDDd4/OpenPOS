using Newtonsoft.Json.Linq;
using OpenPOS_Controllers.Services;
using OpenPOS_Settings;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers;

public class PaymentController
{
    private TikkiePaymentService _tikkiePaymentService;

    public PaymentController()
    {
        _tikkiePaymentService = new TikkiePaymentService();
    }

    public Transaction NewTikkieTransaction(int amountInCents)
    {
        // Calling the API for a new Tikkie Transaction
        var response = _tikkiePaymentService.CreatePaymentRequest(amountInCents, ApplicationSettings.CurrentBill.Id,
            $"OpenPOS Payment: {ApplicationSettings.CurrentBill.Id}");
        
        // Return null if the response is null
        if (response == null)
        {
            return null; 
        }
        
        // Parsing the response to a JObject
        var obj = JObject.Parse(response);
        
        // If the response contains an error throw an exception
        if (obj["errors"] != null)
        {
            throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
        }
        
        // Return the transaction
        return new Transaction
        {
            
            PaymentRequestToken = obj["paymentRequestToken"]?.ToString(),
            AmountInCents = (int)obj["amountInCents"]?.ToObject<int>(), //TODO: Fix warning
            TransactionId = obj["referenceId"]?.ToString(),
            Description = obj["description"]?.ToString(),
            Url = obj["url"]?.ToString(),
            ExpiryDate = (DateTime)obj["expiryDate"]?.ToObject<DateTime>(), //TODO: Fix warning
            CreatedDateTime = (DateTime)obj["createdDateTime"]?.ToObject<DateTime>(), //TODO: Fix warning
            Status = obj["status"]?.ToString(),
            NumberOfPayments = (int)obj["numberOfPayments"]?.ToObject<int>(), //TODO: Fix warning
            TotalAmountPayed = (int)obj["totalAmountPaidInCents"]?.ToObject<int>(), //TODO: Fix warning
        };
    }

    public Transaction GetTikkieTransaction(string paymentId)
    {
        // Calling the tikkie API for a transaction
        var response = _tikkiePaymentService.GetTransactionInformation(paymentId);
        
        // Return null if the response is null
        if (response == null)
        {
            return null; 
        }
        
        // Parsing the response to a JObject
        var obj = JObject.Parse(response);
        
        // If the response contains an error throw an exception
        if (obj["errors"] != null) 
        {
            throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
        }
        
        // Return the transaction
        return new Transaction
        {
            PaymentRequestToken = obj["paymentRequestToken"]?.ToString(),
            AmountInCents = (int)obj["amountInCents"]?.ToObject<int>(), //TODO: Fix warning
            TransactionId = obj["referenceId"]?.ToString(),
            Description = obj["description"]?.ToString(),
            Url = obj["url"]?.ToString(),
            ExpiryDate = (DateTime)obj["expiryDate"]?.ToObject<DateTime>(), //TODO: Fix warning
            CreatedDateTime = (DateTime)obj["createdDateTime"]?.ToObject<DateTime>(), //TODO: Fix warning
            Status = obj["status"]?.ToString(),
            NumberOfPayments = (int)obj["numberOfPayments"]?.ToObject<int>(), //TODO: Fix warning
            TotalAmountPayed = (int)obj["totalAmountPaidInCents"]?.ToObject<int>(), //TODO: Fix warning
        };
    }
}