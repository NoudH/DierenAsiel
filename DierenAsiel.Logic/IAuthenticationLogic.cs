using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IAuthenticationLogic
    {
        /// <summary>
        /// Checks if the credentials entered are correct and returns a bool.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string username, string password);

        /// <summary>
        /// Creates a user in the database with the specified username and hashedpassword.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void CreateUser(string username, string password);
    }
}
