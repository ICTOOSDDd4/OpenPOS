using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Database.Services.Models;
namespace OpenPOS_Controllers;

public static class OpenPOSAPIController
{
    private static OpenPosAPIService _openPosAPIService = new();
    
    public static async Task<bool> SubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        await _openPosAPIService.SubcribeToPaymentNotifications();
        _openPosAPIService.PaymentNotification += handler;
        return await _openPosAPIService.AddToPaymentListener(paymentId);
    }
    
    public static async Task<bool> UnsubcribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        _openPosAPIService.PaymentNotification -= handler;
        await _openPosAPIService.Disconnect();
        return _openPosAPIService.RemoveFromPaymentListener(paymentId);
    }
    
    public static async Task<bool> SubcribeToOrderNotification(string orderId, EventHandler<OrderEventArgs> handler)
    {
        await _openPosAPIService.SubcribeToNewOrderNotification();
        _openPosAPIService.NewOrderNotification += handler;
        return true;
    }
    
    public static async Task<bool> UnsubcribeToOrderNotification(string orderId, EventHandler<OrderEventArgs> handler)
    {
        _openPosAPIService.NewOrderNotification -= handler;
        await _openPosAPIService.Disconnect();
        return true;
    }

    public static string GetCurrentConnectionStatus()
    {
        return _openPosAPIService.ConnectionStatus;
    }
}