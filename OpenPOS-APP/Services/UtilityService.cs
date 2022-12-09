using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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

   public static string HashPassword(string unencrypted)
   {
      //apply SHA256 hash
      using (SHA256 sha256Hash = SHA256.Create())
      {
         // ComputeHash - returns byte array  
         byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(unencrypted));

         // Convert byte array to a string   
         StringBuilder builder = new();
         for (int i = 0; i < bytes.Length; i++)
         {
            builder.Append(bytes[i].ToString("x2"));
         }
         return builder.ToString();
      }
   }
}