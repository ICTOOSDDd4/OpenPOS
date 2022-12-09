using System;
using Newtonsoft.Json.Linq;
using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;
using RestSharp;

namespace OpenPOS_Database.Services.Models
{
    public class TikkiePaymentService
    {
        public string CreatePaymentRequest(int amountInCents, int transactionId, string desc)
        {
            var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
            var request = new RestRequest("/paymentrequests", Method.Post);
            request.AddHeader("X-App-Token", ApplicationSettings.TikkieSet.AppToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            request.AddJsonBody(new
            {
               description = desc,
               amountInCents = amountInCents,
               expiryDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
               referenceId = transactionId.ToString(),
            });

            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content;
            }

            return null;
        }

        public string GetTransactionInformation(string paymentRequestToken)
        {
            var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
            var request = new RestRequest($"/paymentrequests/{paymentRequestToken}");
            request.AddHeader("X-App-Token", ApplicationSettings.TikkieSet.AppToken);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            
            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                return response.Content;
                
            }

            return null;
        }
    }
}