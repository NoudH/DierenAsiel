using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IUiLogic
    {
        List<string> TodoToday();
        List<string> AppointmentsToday();
    }
}
