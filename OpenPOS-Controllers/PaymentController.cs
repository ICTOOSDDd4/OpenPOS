using Newtonsoft.Json.Linq;
using OpenPOS_APP.Settings;
using OpenPOS_Database.Services.Models;
using OpenPOS_Models;

namespace OpenPOS_Controllers;

public static class PaymentController
{
    private static TikkiePaymentService _tikkiePaymentService = new();
    public static Transaction NewTikkieTransaction(int amountInCents)
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
            AmountInCents = (int)obj["amountInCents"]?.ToObject<int>(),
            TransactionId = obj["referenceId"]?.ToString(),
            Description = obj["description"]?.ToString(),
            Url = obj["url"]?.ToString(),
            ExpiryDate = (DateTime)obj["expiryDate"]?.ToObject<DateTime>(),
            CreatedDateTime = (DateTime)obj["createdDateTime"]?.ToObject<DateTime>(),
            Status = obj["status"]?.ToString(),
            NumberOfPayments = (int)obj["numberOfPayments"]?.ToObject<int>(),
            TotalAmountPayed = (int)obj["totalAmountPaidInCents"]?.ToObject<int>(),
        };
    }

    public static Transaction GetTikkieTransaction(string paymentID)
    {
        // Calling the tikkie API for a transaction
        var response = _tikkiePaymentService.GetTransactionInformation(paymentID);
        
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
            AmountInCents = (int)obj["amountInCents"]?.ToObject<int>(),
            TransactionId = obj["referenceId"]?.ToString(),
            Description = obj["description"]?.ToString(),
            Url = obj["url"]?.ToString(),
            ExpiryDate = (DateTime)obj["expiryDate"]?.ToObject<DateTime>(),
            CreatedDateTime = (DateTime)obj["createdDateTime"]?.ToObject<DateTime>(),
            Status = obj["status"]?.ToString(),
            NumberOfPayments = (int)obj["numberOfPayments"]?.ToObject<int>(),
            TotalAmountPayed = (int)obj["totalAmountPaidInCents"]?.ToObject<int>(),
        };
    }
}