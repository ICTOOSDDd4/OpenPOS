using System.Net;
using System.Reflection;
using OpenPOS_APP.Settings;

namespace OpenPOS_APP.Services;

public class UtilityService
{
    public static ImageSource GenerateQrCodeFromUrl(string url)
    {
        string filename = $"{GetRootDirectory()}/qr.png";
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + url;
        
        using (WebClient client = new WebClient())
        {
               client.DownloadFile(new Uri(apiUrl), filename);
        }
        return ImageSource.FromFile(filename);
    }

   public static string GetRootDirectory()
   {
      return AppDomain.CurrentDomain.BaseDirectory;
   }
}