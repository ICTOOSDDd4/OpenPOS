using Microsoft.AspNetCore.SignalR.Client;
using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services
{   
   public class EventHubService
   {
      private readonly CancellationTokenSource _cancelToken = new CancellationTokenSource();
      private readonly string _url = ApplicationSettings.ApiSet.base_url;
      private readonly string _secret = ApplicationSettings.ApiSet.secret;
      private HubConnection _connection;
      private bool _isConnected = false; //Stays here for future possible use.
      private bool _connectionStopped = true;
      public string ConnectionStatus = "Closed"; //Stays public for future possible use.

      public event EventHandler<OrderEventArgs> NewOrder;
      public event EventHandler<PaymentEventArgs> NewPayment;

      public async Task Stop()
      {
         _connectionStopped = false;
         ConnectionStatus = "Disconnected";
         await _connection.StopAsync();
      }

      public async Task ConnectToServer()
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

      public async Task ConnectToServerPayment()
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
   
      public string GetConnectionId()
      {
         return _connection.ConnectionId;
      }

      private void OnNewOrder(Order order)
      {
         NewOrder?.Invoke(this, new OrderEventArgs() { order = order });
      }

      private void OnNewPayment(Tikkie tikkie)
      {
         NewPayment?.Invoke(this, new PaymentEventArgs() { Tikkie = tikkie });
      }
      }
}
