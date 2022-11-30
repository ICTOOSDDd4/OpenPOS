using OpenPOS_APP.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;
using RestSharp;

namespace OpenPOS_APP.Services.Models
{
    public static class PaymentService
    {
        public static PaymentMethod PayMethod;
        
        
        public static string GetPaymentLink(int amountInCents, int transactionId, string desc)
        {
            var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
            var request = new RestRequest("/paymentrequests", Method.Post);
            request.AddHeader("X-App-Token", PayMethod.TikkieAppToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            request.AddJsonBody(new
            {
                description = desc,
                amountInCents = amountInCents,
                expiryDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                referenceId = transactionId,
            });
            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                var obj = JObject.Parse(response.Content);
                if (obj["errors"] != null) // If API returns a error.
                {
                    throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
                }
                return obj["url"]?.ToString();
            }

            return null;
        }

        public static Transaction GetTransactionInformation(string paymentRequestToken)
        {
            var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
            var request = new RestRequest($"/paymentrequests/{paymentRequestToken}");
            request.AddHeader("X-App-Token", PayMethod.TikkieAppToken);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            
            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                var obj = JObject.Parse(response.Content);
                if (obj["errors"] != null) // If API returns a error.
                {
                    throw new Exception($"Error: {obj["errors"][0]?["message"]} ");
                }
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

            return null;
        }
    }
}