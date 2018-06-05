using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IUserDatabase
    {
        /// <summary>
        /// Adds a user with username and password to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        void AddUser(string username, string hashedPassword);

        /// <summary>
        /// Returns the hashedpassword of a user. 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string GetUserPassword(string username);
    }
}
