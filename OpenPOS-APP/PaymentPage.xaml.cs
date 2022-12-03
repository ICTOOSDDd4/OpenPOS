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
		
		QRCode.Source = UtilityService.GenerateQrCodeFromUrl(CurrentTransaction.Url);
	}

	public static void SetTransaction(Transaction transaction, int numberOfRequiredPayments)
	{
		CurrentTransaction = transaction;
		RequiredPayments = numberOfRequiredPayments;
		
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
       PaymentStatus.Text = "Checking payment status...";
       bool status = IsPaymentComplete();

       if (status)
       {
           PaymentStatus.Text = $"Payment complete! {CurrentlyPaid} / {RequiredPayments} payments received.";
       }
       else
       {
           PaymentStatus.Text = $"Payment incomplete! {CurrentlyPaid} out of {RequiredPayments}";
       }
   }

   private bool IsPaymentComplete()
	{
		var transactionInformation = TikkiePayementService.GetTransactionInformation(CurrentTransaction.PaymentRequestToken);
		
		CurrentlyPaid = transactionInformation.NumberOfPayments;
		
		return transactionInformation.NumberOfPayments >= RequiredPayments;
	}
}
