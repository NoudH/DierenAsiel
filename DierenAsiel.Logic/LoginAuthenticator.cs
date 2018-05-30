using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;
using static DierenAsiel.Logic.Modes;

namespace DierenAsiel.Logic
{
    public class LoginAuthenticator : IAuthenticationLogic
    {
        IUserDatabase database;

        public LoginAuthenticator(Mode mode)
        {
            if (mode == Mode.Normal)
            {
                database = Databases.productionDatabase;
            }
            else if (mode == Mode.Test)
            {
                database = Databases.testDatabase;
            }
        }

        public void CreateUser(string username, string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkfd2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkfd2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            database.AddUser(username, Convert.ToBase64String(hashBytes));
        }

        public bool Login(string username, string password)
        {
            string savedPasswordHash = database.GetUserPassword(username);
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
