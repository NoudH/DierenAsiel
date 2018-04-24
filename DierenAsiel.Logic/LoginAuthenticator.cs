using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    static class LoginAuthenticator
    {
        public static string CreateUser(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkfd2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkfd2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool LoginUser(string password, string hashedPassword)
        {
            string savedPasswordHash = hashedPassword;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkfd2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkfd2.GetBytes(20);

            try
            {
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
            }            
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
