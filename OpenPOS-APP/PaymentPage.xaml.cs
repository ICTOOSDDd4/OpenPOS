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
   private Thread thread;
   private string paymentStatusString;
   System.Timers.Timer _timer = new System.Timers.Timer(500);
   private int CurrentlyPaid { get; set; }
   private OpenPOSAPIController _openPOSAPIController = new OpenPOSAPIController();
	public PaymentPage()
	{
      InitializeComponent();
      Connect();
   }
   public async void Connect()
   {
      bool AddedToPaymentListener = await _openPOSAPIController.SubcribeToPaymentNotification(CurrentTransaction.PaymentRequestToken, OnPaymentPayed);
      if (!AddedToPaymentListener)
      {
         throw new Exception("Can't add the Transaction to the Payment Listener");
      }
      ImageSource imageSource = UtilityService.GenerateQrCodeFromUrl(CurrentTransaction.Url);
      
      // Deleting the loader from the screen.
      Loader.IsVisible = false;
      Loader.IsRunning = false;

      // Placing the QR code on the page.
      QRCode.IsVisible = true;
      QRCode.Source = imageSource;

      if (RequiredPayments == 1)
      {
         paymentStatusString = "payment";
      }
      else { paymentStatusString = "payments"; }
      thread = new Thread(new ThreadStart(StartStatusLabel));
      thread.Start();

   }
   private void StartStatusLabel()
   {
      _timer.Elapsed += ChangeStatusLabel;
      _timer.Start();
   }
	public void OnPaymentPayed(object? sender, PaymentEventArgs e)
	{
      _timer.Stop();
      thread.Interrupt();
      Dispatcher.DispatchAsync(async () =>
      {
         CurrentlyPaid++;
         Debug.WriteLine("Payed");
         if (CurrentlyPaid >= RequiredPayments)
         {
            PaymentStatusLabel.Text = $"Payment complete!";
            await Shell.Current.GoToAsync(nameof(GoodbyePage));
            await _openPOSAPIController.UnsubcribeToPaymentNotification(CurrentTransaction.PaymentRequestToken, OnPaymentPayed); //TODO: Move to background task
         }
         else
         {
            PaymentStatusLabel.Text = $"Almost there! {CurrentlyPaid} out of {RequiredPayments}";
         }
      });
      
	}

   public void ChangeStatusLabel(object sender, object e)
   {
      string newString = "";
      Dispatcher.DispatchAsync(async () =>
      {
         if (PaymentStatusLabel.Text.Contains("..."))
         {
            newString = $"Waiting for {paymentStatusString}.";
         } else if (PaymentStatusLabel.Text.Contains(".."))
         {
            newString = $"Waiting for {paymentStatusString}...";
         } else if (PaymentStatusLabel.Text.Contains("."))
         {
            newString = $"Waiting for {paymentStatusString}..";
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
