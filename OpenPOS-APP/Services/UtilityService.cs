using System.Net;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services;

public class UtilityService
{
    public static ImageSource GenerateQrCodeFromUrl(string url)
    {
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + url;

        using (WebClient client = new WebClient())
        {
            client.DownloadFile(new Uri(apiUrl), "qr.png");
        }
		
        return ImageSource.FromFile("qr.png");
    }
}