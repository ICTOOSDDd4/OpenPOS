using System.Net;
using OpenPOS_APP.Services;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP;

public partial class PaymentPage : ContentPage
{
	public PaymentPage()
	{
		InitializeComponent();
		
		QRCode.Source = UtilityService.GenerateQrCodeFromUrl("https://www.google.com"); //TODO: PLACE TIKKIE URL HERE
	}
}
