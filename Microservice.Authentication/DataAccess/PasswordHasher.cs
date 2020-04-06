using FCClone.Microservice.Authentication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FCClone.Microservice.Authentication.DataAccess
{
    public class PasswordHasher : IPasswordHasher
    {
        private int SaltSize { get; } = 128 / 8;
        private int IterationCount { get; } = 20000;
        private int KeySize { get; } = 128 / 8;
        private HashAlgorithmName HashAlgorithmName { get; } = HashAlgorithmName.SHA512;

        public byte[] GenerateSalt(int saltSize)
        {
            var salt = new byte[saltSize];
            using (RNGCryptoServiceProvider rngCryptoProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoProvider.GetBytes(salt);
            }
            return salt;
        }
        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt(SaltSize);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount, HashAlgorithmName);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            return string.Format("{0}.{1}.{2}", IterationCount, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split(".");
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] originalHash = Convert.FromBase64String(parts[2]);

            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount, HashAlgorithmName);
            byte[] hash = pbkdf2.GetBytes(KeySize);

            return hash.SequenceEqual(originalHash);
        }
    }
}
