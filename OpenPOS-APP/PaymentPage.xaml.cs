using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public static Transaction CurrentTransaction { get; set; }
	public static int RequiredPayments { get; set; }
	private static EventHubService _eventHubService = new EventHubService();
	private int CurrentlyPaid { get; set; }
	public PaymentPage()
	{
		InitializeComponent();
      _eventHubService.newPayent += OnPaymentPayed;
      QRCode.Source = UtilityService.GenerateQrCodeFromUrl(CurrentTransaction.Url);
	}

	public void OnPaymentPayed(object sender, PaymentEventArgs e)
	{
      CurrentlyPaid++;
      Debug.WriteLine("Payed");
      if (CurrentlyPaid >= getRequiredPayment())
		{
         PaymentStatus.Text = $"Payment complete! {getRequiredPayment()}";
         ToGoodbyePage();
      } else
		{
         PaymentStatus.Text = $"Almost there! {CurrentlyPaid} out of {getRequiredPayment()}";
      }
	}

	private async void ToGoodbyePage()
	{
		await Shell.Current.GoToAsync(nameof(GoodbyePage));
	}

	public static async Task SetTransaction(Transaction transaction, int numberOfRequiredPayments)
	{
		CurrentTransaction = transaction;
		RequiredPayments = numberOfRequiredPayments;
      await ApiConnentAsync();
      Debug.WriteLine(transaction.PaymentRequestToken);
      if (!OpenPosAPIService.AddToPaymentListener(_eventHubService.GetConnectionID(), transaction.PaymentRequestToken))
      {
         throw new Exception("Oopsie");
      }
   }

   private static async Task ApiConnentAsync()
	{
      await _eventHubService.ConnectToServerPayment();
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
