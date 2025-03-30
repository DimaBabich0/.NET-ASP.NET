using System.Security.Cryptography;
using System.Text;

namespace HW09.Controllers
{
    public class MyCryptography
    {
        static public string GetSalt()
        {
            byte[] saltbuf = new byte[16];
            RandomNumberGenerator rndNumGen = RandomNumberGenerator.Create();
            rndNumGen.GetBytes(saltbuf);
            StringBuilder salt = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
            {
                salt.Append(string.Format("{0:X2}", saltbuf[i]));
            }
            return salt.ToString();
        }

        static public string GenerateHash(string salt, string password)
        {
            byte[] encodingPassword = Encoding.Unicode.GetBytes(salt + password);
            var md5 = MD5.Create();
            byte[] byteHash = md5.ComputeHash(encodingPassword);
            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
            {
                hash.Append(string.Format("{0:X2}", byteHash[i]));
            }
            return hash.ToString();
        }
    

    }
}
