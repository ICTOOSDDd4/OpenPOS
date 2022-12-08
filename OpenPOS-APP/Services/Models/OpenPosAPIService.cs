using Newtonsoft.Json.Linq;
using OpenPOS_APP.Settings;
using RestSharp;
namespace OpenPOS_APP.Services.Models
{
   public static class OpenPosAPIService
   {
      public static async Task<bool> AddToPaymentListener(string connectionID, string PaymentRequestToken)
      {
         var client = new RestClient(ApplicationSettings.ApiSet.base_url);
         var request = new RestRequest("/api/Tikkie/AddToPaymentListener", Method.Post);
         request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
         request.AddHeader("Content-Type", "application/json");
         request.AddHeader("Accept", "application/json");
         request.AddJsonBody(new
         {
            paymentRequestToken = PaymentRequestToken,
            connectionId = connectionID
         });


         RestResponse respone = await client.ExecuteAsync(request); ;
         var content = respone.IsSuccessful;
         if (content)
         {
            return true;
         }
         return false;
      }

      public static async Task<bool> RemoveFromPaymentListener(string PaymentRequestToken)
      {
         var client = new RestClient(ApplicationSettings.ApiSet.base_url);
         var request = new RestRequest("/api/Tikkie/AddToPaymentListener", Method.Post);
         request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
         request.AddHeader("Content-Type", "application/json");
         request.AddHeader("Accept", "application/json");
         request.AddHeader("paymentRequestToken", PaymentRequestToken);


         RestResponse respone = await client.ExecuteAsync(request); ;
         var content = respone.IsSuccessful;
         if (content)
         {
            return true;
         }
         return false;
      }
   }
}
