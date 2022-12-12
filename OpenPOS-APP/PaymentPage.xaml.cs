using System.Diagnostics;
using OpenPOS_Controllers;
using OpenPOS_Controllers.Services;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;


namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
    public static Transaction CurrentTransaction { get; set; }
    public static int RequiredPayments { get; set; } 

    private Thread _thread;
    private string _paymentStatusString;
    private readonly System.Timers.Timer _timer = new(500);
    private int _currentlyPaid { get; set; }
    private readonly OpenPosApiController _openPosApiController = new();
    private PaymentController _paymentController;

    public PaymentPage()
    {
        InitializeComponent();
        Connect();
        _paymentController = new PaymentController();
    }

    public async void Connect()
    {
        bool addedToPaymentListener =
            await _openPosApiController.SubscribeToPaymentNotification(CurrentTransaction.PaymentRequestToken,
                OnPaymentPayed);

        if (!addedToPaymentListener)
        {
            throw new Exception("Can't add the Transaction to the Payment Listener");
        }

        ImageSource imageSource = _paymentController.GetPaymentQR(CurrentTransaction.Url);

        // Deleting the loader from the screen.
        Loader.IsVisible = false;
        Loader.IsRunning = false;

        // Placing the QR code on the page.
        QRCode.IsVisible = true;
        QRCode.Source = imageSource;

        if (RequiredPayments == 1)
        {
            _paymentStatusString = "payment";
        }
        else
        {
            _paymentStatusString = "payments";
        }

        _thread = new Thread(StartStatusLabel);
        _thread.Start();
    }

    private void StartStatusLabel()
    {
        _timer.Elapsed += ChangeStatusLabel;
        _timer.Start();
    }

    public void OnPaymentPayed(object? sender, PaymentEventArgs e)
    {
        _timer.Stop();
        _thread.Interrupt();
        Dispatcher.DispatchAsync(async () =>
        {
            _currentlyPaid++;
            Debug.WriteLine("Payed");
            if (_currentlyPaid >= RequiredPayments)
            {
                PaymentStatusLabel.Text = $"Payment complete!";
                await Shell.Current.GoToAsync(nameof(GoodbyePage));
                await _openPosApiController.UnsubscribeToPaymentNotification(CurrentTransaction.PaymentRequestToken,
                    OnPaymentPayed); //TODO: Move to background task
            }
            else
            {
                PaymentStatusLabel.Text = $"Almost there! {_currentlyPaid} out of {RequiredPayments}";
            }
        });
    }

    public void ChangeStatusLabel(object sender, object e)
    {
        string newString = "";
        Dispatcher.DispatchAsync(() =>
        {
            if (PaymentStatusLabel.Text.Contains("..."))
            {
                newString = $"Waiting for {_paymentStatusString}.";
            }
            else if (PaymentStatusLabel.Text.Contains(".."))
            {
                newString = $"Waiting for {_paymentStatusString}...";
            }
            else if (PaymentStatusLabel.Text.Contains("."))
            {
                newString = $"Waiting for {_paymentStatusString}..";
            }

            PaymentStatusLabel.Text = newString;
        });
    }

    public static void SetTransaction(Transaction transaction, int numberOfRequiredPayments)
    {
	    CurrentTransaction = transaction;
	    RequiredPayments = numberOfRequiredPayments;
        Debug.WriteLine(transaction.PaymentRequestToken);
    }

    public void RemoveQrCodeFile()
    {
        File.Delete($"{UtilityService.GetRootDirectory()}/qr-{ApplicationSettings.CurrentBill.Id}.png");
    }
}