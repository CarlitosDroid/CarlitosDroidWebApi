
using System.Security.Cryptography;
using System.Text;

namespace CarlitosDroidWebApi.Utils;

public static class Security
{
    public static byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16]; // Adjust the size based on your security requirements
            rng.GetBytes(salt);
            return salt;
        }
    }
}
