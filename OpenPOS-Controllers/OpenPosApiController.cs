using OpenPOS_API;
using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Database.Services.Models;
namespace OpenPOS_Controllers;

public class OpenPosApiController
{
    private OpenPosApiService _openPosApiService = new();

    /// <summary>
    /// Subscribes the EventHandler to the PaymentNotification
    /// </summary>
    /// <param name="paymentId">PaymentId linked to the Bill needed to pay</param>
    /// <param name="handler">EventHandler that needs to be fired</param>
    /// <returns>Bool for succeeded or not</returns>
    public async Task<bool> SubscribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        await _openPosApiService.SubscribeToPaymentNotifications();
        _openPosApiService.PaymentNotification += handler;
        return await _openPosApiService.AddToPaymentListener(paymentId);
    }
    /// <summary>
    /// Unsubscribes the EventHandler to the PaymentNotification
    /// </summary>
    /// <param name="paymentId">PaymentId linked to the Bill needed to pay</param>
    /// <param name="handler">EventHandler that needs to stop being fired</param>
    /// <returns>Bool for succeeded or not</returns>
    public async Task<bool> UnsubscribeToPaymentNotification(string paymentId, EventHandler<PaymentEventArgs> handler)
    {
        _openPosApiService.PaymentNotification -= handler;
        await _openPosApiService.Disconnect();
        return _openPosApiService.RemoveFromPaymentListener(paymentId);
    }

    /// <summary>
    /// Subscribes the EventHandler to the OrderNotification
    /// </summary>
    /// <param name="handler">EventHandler that needs to be fired</param>
    /// <returns>Bool for succeeded or not</returns>
    public async Task<bool> SubscribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        await _openPosApiService.SubscribeToNewOrderNotification();
        _openPosApiService.NewOrderNotification += handler;
        System.Diagnostics.Debug.WriteLine(_openPosApiService.ConnectionStatus);
        return true;
    }

    /// <summary>
    /// Unsubscribes the EventHandler to the OrderNotification
    /// </summary>
    /// <param name="handler">EventHandler that needs to stop being fired</param>
    /// <returns>Bool for succeeded or not</returns>
    public async Task<bool> UnsubscribeToOrderNotification(EventHandler<OrderEventArgs> handler)
    {
        _openPosApiService.NewOrderNotification -= handler;
        await _openPosApiService.Disconnect();
        return true;
    }

    /// <summary>
    /// Gets the state the connection is in
    /// </summary>
    /// <returns>String with status</returns>
    public string GetCurrentConnectionStatus()
    {
        return _openPosApiService.ConnectionStatus;
    }
}