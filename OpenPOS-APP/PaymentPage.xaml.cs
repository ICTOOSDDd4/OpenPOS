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
      if (CurrentlyPaid >= RequiredPayments)
		{
         PaymentStatus.Text = $"Payment complete! {CurrentlyPaid} / {RequiredPayments} payments received.";
      } else
		{
         PaymentStatus.Text = $"Almost there! {CurrentlyPaid} out of {RequiredPayments}";
      }
	}

	public static async Task SetTransaction(Transaction transaction, int numberOfRequiredPayments)
	{
		CurrentTransaction = transaction;
		RequiredPayments = numberOfRequiredPayments;
      await ApiConnentAsync();
      Debug.WriteLine(transaction.PaymentRequestToken);
      OpenPosAPIService.AddToPaymentListener(_eventHubService.GetConnectionID(), transaction.PaymentRequestToken);
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

}
