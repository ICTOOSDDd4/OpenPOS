using OpenPOS_APP.EventArgsClasses;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;
using System;
using System.Diagnostics;

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
      if (CurrentlyPaid >= RequiredPayments)
		{
         PaymentStatus.Text = $"Payment complete! {CurrentlyPaid} / {RequiredPayments} payments received.";
         //redirect to GoodbyePage
         
      } else
		{
         PaymentStatus.Text = $"Almost there! {CurrentlyPaid} out of {RequiredPayments}";
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
	}

	private static async Task ApiConnentAsync()
	{
      await _eventHubService.ConnectToServerPayment();
   }

  
  public void RemoveQRCodeFile()
   {
      File.Delete($"{UtilityService.GetRootDirectory()}/qr-{ApplicationSettings.CurrentBill.Id}.png");
   }


   protected override void OnNavigatedTo(NavigatedToEventArgs args)
   {
      base.OnNavigatedTo(args);
   }

   private void OnPaymentStatusCheck_Clicked(object sender, EventArgs e)
   {
       //PaymentStatus.Text = "Checking payment status...";
       //bool status = IsPaymentComplete();

       
   }

}
