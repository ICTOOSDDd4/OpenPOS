using System.Data;
using System.Security.Cryptography;
using System.Text;
using OpenPOS_Database;
using OpenPOS_Settings;

namespace OpenPOS_Controllers.Services;

public class UtilityService
{
    /// <summary>
    /// Generates a QR-code with given URL
    /// </summary>
    /// <param name="URL">URL the QR-code needs to be generated for</param>
    /// <returns>The QR-code as an ImageSource Object</returns>
    public async Task<ImageSource> GenerateQrCodeFromUrl(string URL)
    {
        string filename = $"{GetRootDirectory()}/qr.png";
        string apiUrl = ApplicationSettings.QRCodeGeneratorSet.Base_url + URL;

        Uri uri = new Uri(apiUrl);
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(uri);
        using (var fs = new FileStream(Path.GetFullPath(filename), FileMode.Create))
        {
            await response.Content.CopyToAsync(fs);
        }
        return ImageSource.FromFile(filename);

    }

    /// <summary>
    /// Hashes Password with SHA256
    /// </summary>
    /// <param name="unencrypted">Password inputted by User</param>
    /// <returns>Hashed Password</returns>
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

    /// <summary>
    /// Gets RootDirectory of the application
    /// </summary>
    /// <returns>Path to root of application</returns>
    public static string GetRootDirectory()
    {
       return AppDomain.CurrentDomain.BaseDirectory;
    }

    /// <summary>
    /// Initializes the DatabaseService
    /// </summary>
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