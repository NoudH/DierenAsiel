using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IUserDatabase
    {
        void AddUser(string username, string hashedPassword);
        string GetUserPassword(string username);
    }
}
