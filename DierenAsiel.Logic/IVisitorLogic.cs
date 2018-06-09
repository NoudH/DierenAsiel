using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IVisitorLogic
    {
        void AddAppointment(string appointmentName, string visitor, DateTime value);
        List<Appointment> GetAllAppointments();
        void RemoveAppointment(string name, string visitor, DateTime dateTime);
    }
}
