using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IVisitorDatabase
    {
        void AddAppointment(Appointment appointment);
        List<Appointment> GetAllAppointments();
        void RemoveAppointment(Appointment appointment);
    }
}
