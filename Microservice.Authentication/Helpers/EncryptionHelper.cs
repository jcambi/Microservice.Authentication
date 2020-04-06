using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FCClone.Microservice.Authentication.Helpers
{
    public class EncryptionHelper
    {
        public static string Encrypt(string password)
        {
            var saltSize = 16;
            var keySize = 16;
            var iterations = 10000;
            byte[] key;
            byte[] data = Encoding.UTF32.GetBytes(password);
            using (var algorithm = new Rfc2898DeriveBytes(password, saltSize, iterations, HashAlgorithmName.SHA512))
            {
                key = algorithm.GetBytes(keySize);
            }

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = key;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                byte[] output = tdes.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
                tdes.Clear();
                return Convert.ToBase64String(output, 0, output.Length);
            }
        }

        public string Decrypt(string password)
        {
            var saltSize = 16;
            var keySize = 32;
            var iterations = 10000;
            byte[] key;
            byte[] data = Convert.FromBase64String(password);

            using (var algorithm = new Rfc2898DeriveBytes(password, saltSize, iterations, HashAlgorithmName.SHA512))
            {
                key = algorithm.GetBytes(keySize);
            }

            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.Key = key;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                byte[] output = tdes.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);
                tdes.Clear();
                return UTF32Encoding.UTF32.GetString(output);
            }
        }
    }
}
