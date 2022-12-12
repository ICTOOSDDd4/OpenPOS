using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Database.Services.Models;
namespace OpenPOS_Controllers;

public class OpenPOSAPIController
{
    private OpenPosAPIService _openPosAPIService = new();
    delegate void OpenPosAPIServiceEventHandler(object sender, PaymentEventArgs e);
    public async Task<bool> SubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        await _openPosAPIService.SubcribeToPaymentNotifications();
        _openPosAPIService.PaymentNotification += handler;
        return await _openPosAPIService.AddToPaymentListener(paymentId);
    }
    
    public async Task<bool> UnsubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        _openPosAPIService.PaymentNotification -= handler;
        await _openPosAPIService.Disconnect();
        return _openPosAPIService.RemoveFromPaymentListener(paymentId);
    }
    
    public async Task<bool> SubcribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        await _openPosAPIService.SubcribeToNewOrderNotification();
        _openPosAPIService.NewOrderNotification += handler;
        return true;
    }
    
    public async Task<bool> UnsubcribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        _openPosAPIService.NewOrderNotification -= handler;
        await _openPosAPIService.Disconnect();
        return true;
    }

    public string GetCurrentConnectionStatus()
    {
        return _openPosAPIService.ConnectionStatus;
    }
}