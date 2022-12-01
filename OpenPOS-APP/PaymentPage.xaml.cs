using System.Net;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Services.Models;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public static Transaction CurrentTransaction { get; set; }
	public static int RequiredPayments { get; set; }
	
	private int CurrentlyPaid { get; set; }
	public PaymentPage()
	{
		InitializeComponent();
		
		QRCode.Source = UtilityService.GenerateQrCodeFromUrl("https://www.google.com"); //TODO: PLACE TIKKIE URL HERE
	}

	public static void SetTransaction(Transaction transaction, int numberOfRequiredPayments)
	{
		CurrentTransaction = transaction;
		RequiredPayments = numberOfRequiredPayments;
	}

	private async void OnPaymentStatusCheck_Clicked(object sender, EventArgs e)
	{ 
		bool status = IsPaymentComplete();
	}
	
	private bool IsPaymentComplete()
	{
		var transactionInformation = TikkiePayementService.GetTransactionInformation(CurrentTransaction.PaymentRequestToken);
		
		CurrentlyPaid = transactionInformation.NumberOfPayments;
		
		return transactionInformation.NumberOfPayments >= RequiredPayments;
	}
}
