using System.Diagnostics;
using OpenPOS_Controllers;
using OpenPOS_Controllers.Services;
using OpenPOS_Models;
using OpenPOS_Settings;
using OpenPOS_Settings.EventArgsClasses;
using OpenPOS_Settings.Exceptions;


namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
   private static Transaction CurrentTransaction { get; set; }
   private static int RequiredPayments { get; set; } 
   private Thread _thread;
   private string _paymentStatusString;
   private readonly System.Timers.Timer _timer = new System.Timers.Timer(500);
   private int CurrentlyPaid { get; set; }
   private readonly OpenPosApiController _openPosApiController = new OpenPosApiController();
	public PaymentPage()
	{
      InitializeComponent();
      Connect();
   }

   private async void Connect() // Connects to the OpenPOS API
   {
      try
      {
         bool addedToPaymentListener = await _openPosApiController.SubscribeToPaymentNotification(CurrentTransaction.PaymentRequestToken, OnPaymentPayed);
         if (!addedToPaymentListener)
         {
            throw new Exception("Can't add the Transaction to the Payment Listener");
         }
      } catch (Exception e)
      {
         ExceptionHandler.HandleException(e, this, true, true);
      }

      UtilityService utility = new UtilityService();

      ImageSource imageSource = await utility.GenerateQrCodeFromUrl(CurrentTransaction.Url);
      
      // Deleting the loader from the screen.
      Loader.IsVisible = false;
      Loader.IsRunning = false;

      // Placing the QR code on the page.
      QRCode.IsVisible = true;
      QRCode.Source = imageSource;

      if (RequiredPayments == 1) // Keeping the if to enhance readability
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

   private void OnPaymentPayed(object sender, PaymentEventArgs e)
	{
      if (sender == null) throw new ArgumentNullException(nameof(sender));
      _timer.Stop();
      _thread.Interrupt();
      Dispatcher.DispatchAsync(async () =>
      {
         CurrentlyPaid++;
         Debug.WriteLine("Payed");
         if (CurrentlyPaid >= RequiredPayments)
         {
            PaymentStatusLabel.Text = $"Payment complete!";
            await Shell.Current.GoToAsync(nameof(GoodbyePage));
            await _openPosApiController.UnsubscribeToPaymentNotification(CurrentTransaction.PaymentRequestToken, OnPaymentPayed); //TODO: Move to background task
         }
         else
         {
            PaymentStatusLabel.Text = $"Almost there! {CurrentlyPaid} out of {RequiredPayments}";
         }
      });
      
	}

   private void ChangeStatusLabel(object sender, object e)
   {
      string newString = "";
      Dispatcher.DispatchAsync(() =>
      {
         if (PaymentStatusLabel.Text.Contains("..."))
         {
            newString = $"Waiting for {_paymentStatusString}.";
         } else if (PaymentStatusLabel.Text.Contains(".."))
         {
            newString = $"Waiting for {_paymentStatusString}...";
         } else if (PaymentStatusLabel.Text.Contains("."))
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
   public void RemoveQrCodeFile() // Removes the QR code file from the local storage, a future option that can be checked by the admin, per connected device.
   {
      File.Delete($"{UtilityService.GetRootDirectory()}/qr-{ApplicationSettings.CurrentBill.Id}.png");
   }

}