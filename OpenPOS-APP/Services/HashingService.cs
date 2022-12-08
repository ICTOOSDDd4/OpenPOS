using System.Security.Cryptography;
using System.Text;

namespace OpenPOS_APP.Services;

public class HashingService
{
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