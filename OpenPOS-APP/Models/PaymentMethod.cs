using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using RestSharp;

namespace OpenPOS_APP.Models
{
    public class PaymentMethod
    {
        public string TikkieAppToken { get; set; }

        public PaymentMethod()
        {
            CreateTikkiePaymentMethod();
        }

        public void CreateTikkiePaymentMethod()
        {
            var client = new RestClient(ApplicationSettings.TikkieSet.BaseUrl);
				
            var request = new RestRequest("/sandboxapps", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("API-Key", ApplicationSettings.TikkieSet.Key);
            RestResponse response = client.Execute(request);
            var content = response.Content;
				
            if (content != null)
            {
                var obj = JObject.Parse(content);
                TikkieAppToken = obj["appToken"]?.ToString();
            } else Debug.WriteLine("No content");
        }


    }
}
