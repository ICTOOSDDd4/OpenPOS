using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services
{
   public class EventHubService
   {
      private readonly string _url = ApplicationSettings.ApiSet.base_url;
      private readonly string _secret = ApplicationSettings.ApiSet.secret;
      private HubConnection _connection = null;
      public bool _isConnected = false;
      public string _connectionStatus = "Closed";
      private List<Order> TestOrder = new List<Order>();

      public event EventHandler<OrderEventArgs> newOrder;
      public event EventHandler<PaymentEventArgs> newPayent;

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
            _connectionStatus = "Connected";
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex);
         }

         _connection.Closed += async (s) =>
         {
            _isConnected = false;
            _connectionStatus = "Disconnected";
            await _connection.StartAsync();
            _isConnected = true;
         };

         _connection.On<Order>("newOrder", async (Order m) =>  {  OnNewOrder(m); });

         _connection.On<Tikkie>("PaymentConformation", async (Tikkie t) => { OnNewPayment(t); });
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
            _connectionStatus = "Connected";
         }
         catch (Exception ex)
         {
            System.Diagnostics.Debug.WriteLine(ex);
         }

         _connection.Closed += async (s) =>
         {
            _isConnected = false;
            _connectionStatus = "Disconnected";
            await _connection.StartAsync();
            _isConnected = true;
         };

         _connection.On<Tikkie>("PaymentConformation", async (Tikkie t) => { OnNewPayment(t); });
      }
   

      protected void OnNewOrder(Order order)
      {
         newOrder?.Invoke(this, new OrderEventArgs() { order = order });
      }

      protected void OnNewPayment(Tikkie tikkie)
      {
         newPayent?.Invoke(this, new PaymentEventArgs() { Tikkie = tikkie });
      }


   }
}