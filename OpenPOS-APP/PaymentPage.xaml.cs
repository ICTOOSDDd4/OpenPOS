using System.Net;
using Microsoft.UI.Windowing;
using OpenPOS_APP.Models;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;
using OpenPOS_APP.WinUI;
using Windows.UI.WindowManagement;

namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
    public static Transaction CurrentTransaction { get; set; }
    public static int RequiredPayments { get; set; }


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
}
