using System.Security.Cryptography;
using System.Text;

namespace Framework.Auth
{
    public class Encryption
    {
        public static string HashSha256(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //ComputeHash -return byte array
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                //Convert bye to array
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
