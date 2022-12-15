using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;
using RestSharp;

namespace OpenPOS_API
{
   public class OpenPosApiService
   {
      private readonly CancellationTokenSource _cancelToken = new ();
      private readonly string _url = ApplicationSettings.ApiSet.base_url;
      private readonly string _secret = ApplicationSettings.ApiSet.secret;

      private HubConnection _connection;
      
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
             paymentRequestToken,
             connectionId = GetConnectionId()
         });
         
         RestResponse response = await client.ExecuteAsync(request);
         return response.IsSuccessful;
      }
      public async Task<bool> NewOrderRequest(Order order)
      {
          var client = new RestClient(ApplicationSettings.ApiSet.base_url);
          var request = new RestRequest("/api/order/newOrder", Method.Post);
          request.AddHeader("secret", ApplicationSettings.ApiSet.secret);
          request.AddHeader("Content-Type", "application/json");
          request.AddHeader("Accept", "application/json");
          request.AddJsonBody(order);

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


         RestResponse response = client.Execute(request);
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
         _connection = null;
      }

      public async Task SubscribeToNewOrderNotification()
      {
         Debug.WriteLine("Subscribing to new order notification");
         if (_connection == null)
         {
            _connection = new HubConnectionBuilder()
               .WithUrl(_url + "/order_event", (conn) =>
               {
                  conn.Headers.Add("secret", _secret);
               })
               .Build();
            try
            {
               await _connection.StartAsync();
               ConnectionStatus = "Connected";
            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex);
               Debug.WriteLine("API Oopsie");
            }

            _connection.Closed += async (_) =>
            {
               if (_connectionStopped)
               {
                  ConnectionStatus = "Disconnected";
                  await _connection.StartAsync();
               } else
               {
                  _cancelToken.Cancel();
               }
            };

            _connection.On<Order>("newOrder", OnNewOrder);
         }
         else
         {
            Debug.WriteLine("problem");
            throw new Exception("Connection is already connected, you have to disconnect first.");
         }
         
      }
      
      public async Task SubscribeToPaymentNotifications()
      {
         if (_connection == null)
         {
            _connection = new HubConnectionBuilder()
               .WithUrl(_url + "/tikkie_event", (conn) =>
               {
                  conn.Headers.Add("secret", _secret);
               })
               .Build();
            try
            {
               await _connection.StartAsync();
               ConnectionStatus = "Connected";
            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex);
               Debug.WriteLine("API Oopsie");
            }

            _connection.Closed += async (_) =>
            {
               if (_connectionStopped)
               {
                  ConnectionStatus = "Disconnected";
                  await _connection.StartAsync();
               }
               else
               {
                  _cancelToken.Cancel();
               }
            };

            _connection.On<Tikkie>("PaymentConformation",  OnNewPayment);
         }
         else
         {
            throw new Exception("Connection is already connected, you have to disconnect first.");
         }
         
      }
      
      private void OnNewOrder(Order order)
      {
         Debug.WriteLine(order.Id);
         NewOrderNotification?.Invoke(this, new OrderEventArgs() { order = order });
      }

      private void OnNewPayment(Tikkie tikkie)
      {
         Debug.WriteLine(tikkie.paymentRequestToken);
         PaymentNotification?.Invoke(this, new PaymentEventArgs() { Tikkie = tikkie });
      }
      
   }
}
