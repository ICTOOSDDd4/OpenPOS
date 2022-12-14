using System.Data;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OpenPOS_Database;
using OpenPOS_Settings;

namespace OpenPOS_Controllers.Services;

public class UtilityService
{
    public ImageSource GenerateQrCodeFromUrl(string url)
    {
        string filename = $"{GetRootDirectory()}/qr.png";
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + url;
        
        using (WebClient client = new WebClient())
        {
               client.DownloadFile(new Uri(apiUrl), filename);
        }
        return ImageSource.FromFile(filename);
    }

    public string HashPassword(string unencrypted)
    {
        // ComputeHash - returns byte array  
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(unencrypted));

        // Convert byte array to a string   
        StringBuilder builder = new();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }

    public static string GetRootDirectory()
    {
       return AppDomain.CurrentDomain.BaseDirectory;
    }

    public static void StartDatabase()
    {
        DatabaseService.Initialize();
        DatabaseService.Dbcontext.Open();
        if (DatabaseService.Dbcontext.State == ConnectionState.Open)
        { 
            DatabaseService.Dbcontext.Close();
        }
    }
}