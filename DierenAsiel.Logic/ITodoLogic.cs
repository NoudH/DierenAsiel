using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IUiLogic
    {
        /// <summary>
        /// Creates a Todo list for today.
        /// </summary>
        /// <returns></returns>
        List<string> TodoToday();

        /// <summary>
        /// Gets all appointments planned for today.
        /// </summary>
        /// <returns></returns>
        List<string> AppointmentsToday();
    }
}
