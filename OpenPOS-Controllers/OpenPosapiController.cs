using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Database.Services.Models;
namespace OpenPOS_Controllers;

public class OpenPosapiController
{
    private OpenPosAPIService _openPosApiService = new();

    public async Task<bool> SubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        await _openPosApiService.SubcribeToPaymentNotifications();
        _openPosApiService.PaymentNotification += handler;
        return await _openPosApiService.AddToPaymentListener(paymentId);
    }
    
    public async Task<bool> UnsubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        _openPosApiService.PaymentNotification -= handler;
        await _openPosApiService.Disconnect();
        return _openPosApiService.RemoveFromPaymentListener(paymentId);
    }
    
    public async Task<bool> SubcribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        await _openPosApiService.SubcribeToNewOrderNotification();
        _openPosApiService.NewOrderNotification += handler;
        return true;
    }
    
    public async Task<bool> UnsubcribeToOrderNotification(EventHandler<OrderEventArgs> handler)
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