using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Diagnostics;


namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public static Transaction CurrentTransaction { get; set; }
	public static int RequiredPayments { get; set; }
	private EventHubService _eventHubService = new EventHubService();
   private EventHandler<EventArgs> _eventHandler;
   private Thread thread;
   private delegate void EventMethod(object sender, EventArgs e);
   private string paymentStatusString;
   System.Timers.Timer _timer = new System.Timers.Timer(500);
   private int CurrentlyPaid { get; set; }
	public PaymentPage()
	{
      InitializeComponent();
      Connect();
      QRCode.Source = UtilityService.GenerateQrCodeFromUrl(CurrentTransaction.Url);

      if (RequiredPayments == 1)
      {
         paymentStatusString = "payment";
      } else { paymentStatusString = "payments"; }

      thread = new Thread(new ThreadStart(StartStatusLabel));
      thread.Start();
   }
   private async void Connect()
   {
      await _eventHubService.ConnectToServerPayment();
      bool AddedToPaymentListener = await OpenPosAPIService.AddToPaymentListener(_eventHubService.GetConnectionID(), CurrentTransaction.PaymentRequestToken);
      if (!AddedToPaymentListener)
      {
         throw new Exception("Oopsie");
      }
      _eventHubService.newPayent += OnPaymentPayed;
        
   }
   private void StartStatusLabel()
   {
      _timer.Elapsed += ChangeStatusLabel;
      _timer.Start();
   }
	public void OnPaymentPayed(object sender, PaymentEventArgs e)
	{
      _timer.Stop();
      Dispatcher.DispatchAsync(async () =>
      {
         CurrentlyPaid++;
         Debug.WriteLine("Payed");
         if (CurrentlyPaid >= getRequiredPayment())
         {
            await _eventHubService.Stop();
            PaymentStatusLabel.Text = $"Payment complete! {getRequiredPayment()}";
            ToGoodbyePage();
         }
         else
         {
            PaymentStatusLabel.Text = $"Almost there! {CurrentlyPaid} out of {getRequiredPayment()}";
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

	private async void ToGoodbyePage()
	{
		await Shell.Current.GoToAsync(nameof(GoodbyePage));
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

   public int getRequiredPayment()
   {
      return RequiredPayments;
   }
   
   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

}
