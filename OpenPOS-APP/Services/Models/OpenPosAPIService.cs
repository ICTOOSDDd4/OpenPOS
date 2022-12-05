using Newtonsoft.Json.Linq;
using OpenPOS_APP.Settings;
using RestSharp;
namespace OpenPOS_APP.Services.Models
{
    public static class OpenPosAPIService
    {
      public static bool AddToPaymentListener(string connectionID, string PaymentRequestToken)
      {
         var client = new RestClient(ApplicationSettings.ApiSet.base_url);
         var request = new RestRequest("/api/AddToPaymentListener");
         request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
         request.AddHeader("connectionId", connectionID);
         request.AddHeader("paymentRequestToken", PaymentRequestToken);

         RestResponse respone = client.Execute(request);
         var content = respone.Content;

         if (content != null)
         {
            return content.ToString() == "Added with success";
         }
         return false;
      }
    }
}
