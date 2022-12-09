using Microsoft.AspNetCore.SignalR.Client;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;
using RestSharp;
namespace OpenPOS_Database.Services.Models
{
   public class OpenPosAPIService
   {
      private readonly CancellationTokenSource _cancelToken = new ();
      private readonly string _url = ApplicationSettings.ApiSet.base_url;
      private readonly string _secret = ApplicationSettings.ApiSet.secret;
      
      private HubConnection _connection;
      
      private bool _isConnected = false; //Stays here for future possible use.
      private bool _connectionStopped = true;
      public string ConnectionStatus = "Closed"; //Stays public for future possible use.
      
      // Events
      public event EventHandler<OrderEventArgs> NewOrderNotification;
      public event EventHandler<PaymentEventArgs> PaymentNotification;
      
      public async Task<bool> AddToPaymentListener(string paymentRequestToken)
      {
         var client = new RestClient(ApplicationSettings.ApiSet.base_url);
         var request = new RestRequest("/api/Tikkie/AddToPaymentListener", Method.Post);
         request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
         request.AddHeader("Content-Type", "application/json");
         request.AddHeader("Accept", "application/json");
         request.AddJsonBody(new
         {
            paymentRequestToken = paymentRequestToken,
            connectionId = GetConnectionId()
         });
         
         RestResponse response = await client.ExecuteAsync(request);
         return response.IsSuccessful;
      }

      public bool RemoveFromPaymentListener(string paymentRequestToken)
      {
         var client = new RestClient(ApplicationSettings.ApiSet.base_url);
         var request = new RestRequest("/api/Tikkie/AddToPaymentListener", Method.Get);
         request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
         request.AddHeader("Content-Type", "application/json");
         request.AddHeader("Accept", "application/json");
         request.AddHeader("paymentRequestToken", paymentRequestToken);


         RestResponse response = client.Execute(request); ;
         return response.IsSuccessful;
      }
      
      private string GetConnectionId()
      {
         return _connection.ConnectionId;
      }
      
      public async Task Disconnect()
      {
         _connectionStopped = false;
         ConnectionStatus = "Disconnected";
         await _connection.StopAsync();
      }
      
      public async Task SubcribeToNewOrderNotification()
      {
         System.Diagnostics.Debug.WriteLine(_secret);
         _connection = new HubConnectionBuilder()
            .WithUrl(_url + "/order_event", (conn) =>
            {
               conn.Headers.Add("secret", _secret);
            })
            .Build();
         try
         {
            await _connection.StartAsync();
            _isConnected = true;
            ConnectionStatus = "Connected";
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex);
         }

         _connection.Closed += async (_) =>
         {
            if (_connectionStopped)
            {
               _isConnected = false;
               ConnectionStatus = "Disconnected";
               await _connection.StartAsync();
               _isConnected = true;
            } else
            {
               _cancelToken.Cancel();
            }
         };

         _connection.On<Order>("newOrder", OnNewOrder);
      }
      
      public async Task SubcribeToPaymentNotifications()
      {
         System.Diagnostics.Debug.WriteLine(_secret);
         _connection = new HubConnectionBuilder()
            .WithUrl(_url + "/tikkie_event", (conn) =>
            {
               conn.Headers.Add("secret", _secret);
            })
            .Build();
         try
         {
            await _connection.StartAsync();
            _isConnected = true;
            ConnectionStatus = "Connected";
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex);
         }

         _connection.Closed += async (_) =>
         {
            if (_connectionStopped)
            {
               _isConnected = false;
               ConnectionStatus = "Disconnected";
               await _connection.StartAsync();
               _isConnected = true;
            }
            else
            {
               _cancelToken.Cancel();
            }
         };

         _connection.On<Tikkie>("PaymentConformation",  OnNewPayment);
      }
      
      private void OnNewOrder(Order order)
      {
         NewOrderNotification?.Invoke(this, new OrderEventArgs() { order = order });
      }

      private void OnNewPayment(Tikkie tikkie)
      {
         PaymentNotification?.Invoke(this, new PaymentEventArgs() { Tikkie = tikkie });
      }
      
   }
}
