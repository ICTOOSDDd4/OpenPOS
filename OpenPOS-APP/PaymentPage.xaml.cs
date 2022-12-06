using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System.Threading;
using System.Diagnostics;
using System.Reflection.Metadata;
using Windows.UI.StartScreen;
using Windows.UI.Core;
using Microsoft.Maui.Dispatching;

namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public static Transaction CurrentTransaction { get; set; }
	public static int RequiredPayments { get; set; }
	private EventHubService _eventHubService = new EventHubService();
   private EventHandler<EventArgs> _eventHandler;
	private int CurrentlyPaid { get; set; }
	public PaymentPage()
	{
      InitializeComponent();
      Connect();
      QRCode.Source = UtilityService.GenerateQrCodeFromUrl(CurrentTransaction.Url);

   }

   private async void Connect()
   {
      await _eventHubService.ConnectToServerPayment();
      if (!OpenPosAPIService.AddToPaymentListener(_eventHubService.GetConnectionID(), CurrentTransaction.PaymentRequestToken))
      {
         throw new Exception("Oopsie");
      }
      _eventHubService.newPayent += OnPaymentPayed;
   }

	public async void OnPaymentPayed(object sender, PaymentEventArgs e)
	{
      await Dispatcher.DispatchAsync(async () =>
      {
         CurrentlyPaid++;
         Debug.WriteLine("Payed");
         if (CurrentlyPaid >= getRequiredPayment())
         {
            await _eventHubService.Stop();
            //_eventHubService = null;
            PaymentStatus.Text = $"Payment complete! {getRequiredPayment()}";
            ToGoodbyePage();
         }
         else
         {
            PaymentStatus.Text = $"Almost there! {CurrentlyPaid} out of {getRequiredPayment()}";
         }
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
