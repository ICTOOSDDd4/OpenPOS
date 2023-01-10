using System.Data;
using System.Security.Cryptography;
using System.Text;
using OpenPOS_Database;
using OpenPOS_Settings;

namespace OpenPOS_Controllers.Services;

public class UtilityService
{
    public async Task<ImageSource> GenerateQrCodeFromUrl(string url)
    {
        string filename = $"{GetRootDirectory()}/qr.png";
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + url;

        Uri uri = new Uri(apiUrl);
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(uri);
        using (var fs = new FileStream(Path.GetFullPath(filename), FileMode.Create))
        {
            await response.Content.CopyToAsync(fs);
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