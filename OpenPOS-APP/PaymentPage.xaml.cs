using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using System.Diagnostics;
using OpenPOS_Controllers.Services;
using OpenPOS_Database.Services.Models;


namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public static Transaction CurrentTransaction { get; set; }
	public static int RequiredPayments { get; set; } 
	private EventHubService _eventHubService = new EventHubService();
   private Thread thread;
   private delegate void EventMethod(object sender, EventArgs e);
   private string paymentStatusString;
   System.Timers.Timer _timer = new System.Timers.Timer(500);
   private int CurrentlyPaid { get; set; }
	public PaymentPage()
	{
      InitializeComponent();
      Connect();
   }
   public async void Connect()
   {
      await _eventHubService.ConnectToServerPayment();
      bool AddedToPaymentListener = await OpenPosAPIService.AddToPaymentListener(_eventHubService.GetConnectionId(), CurrentTransaction.PaymentRequestToken);
      if (!AddedToPaymentListener)
      {
         throw new Exception("Can't add the Transaction to the Payment Listener");
      }
      _eventHubService.NewPayment += OnPaymentPayed;
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
	public void OnPaymentPayed(object sender, PaymentEventArgs e)
	{
      _timer.Stop();
      thread.Interrupt();
      Dispatcher.DispatchAsync(async () =>
      {
         CurrentlyPaid++;
         Debug.WriteLine("Payed");
         if (CurrentlyPaid >= RequiredPayments)
         {
            await _eventHubService.Stop();
            PaymentStatusLabel.Text = $"Payment complete!";
            await Shell.Current.GoToAsync(nameof(GoodbyePage));
            await OpenPosAPIService.RemoveFromPaymentListener(CurrentTransaction.PaymentRequestToken);
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
   public void RemoveQRCodeFile()
   {
      File.Delete($"{UtilityService.GetRootDirectory()}/qr-{ApplicationSettings.CurrentBill.Id}.png");
   }
   
   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

}
