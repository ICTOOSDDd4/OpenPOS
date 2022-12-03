using System.Net;
using System.Reflection;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services;

public class UtilityService
{
    public static ImageSource GenerateQrCodeFromUrl(string url)
    {
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + url;
        
        
        using (WebClient client = new WebClient())
        {
               client.DownloadFile(new Uri(apiUrl), $"{ GetRootDirectory() }/qr-{ApplicationSettings.CurrentBill.Id}.png");
        }
		
        return ImageSource.FromFile("qr.png");
    }

   public static string GetRootDirectory()
   {
      return AppDomain.CurrentDomain.BaseDirectory;
   }
}