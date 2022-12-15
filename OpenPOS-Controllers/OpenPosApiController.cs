using OpenPOS_API;
using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Database.Services.Models;
namespace OpenPOS_Controllers;

public class OpenPosApiController
{
    private OpenPosApiService _openPosApiService = new();

    public async Task<bool> SubscribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        await _openPosApiService.SubscribeToPaymentNotifications();
        _openPosApiService.PaymentNotification += handler;
        return await _openPosApiService.AddToPaymentListener(paymentId);
    }
    
    public async Task<bool> UnsubscribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        _openPosApiService.PaymentNotification -= handler;
        await _openPosApiService.Disconnect();
        return _openPosApiService.RemoveFromPaymentListener(paymentId);
    }
    
    public async Task<bool> SubscribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        await _openPosApiService.SubscribeToNewOrderNotification();
        _openPosApiService.NewOrderNotification += handler;
        System.Diagnostics.Debug.WriteLine(_openPosApiService.ConnectionStatus);
        return true;
    }
    
    public async Task<bool> UnsubscribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        _openPosApiService.NewOrderNotification -= handler;
        await _openPosApiService.Disconnect();
        return true;
    }

    public string GetCurrentConnectionStatus()
    {
        return _openPosApiService.ConnectionStatus;
    }
}