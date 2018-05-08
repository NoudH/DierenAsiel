using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IAuthenticationLogic
    {
        bool Login(string username, string password);
        void CreateUser(string username, string password);
    }
}
