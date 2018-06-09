using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;
using DierenAsiel.Database;

namespace DierenAsiel.Logic
{
    public class VisitorLogic : IVisitorLogic
    {
        IVisitorDatabase database;

        public VisitorLogic(Modes.Mode mode)
        {
            if (mode == Modes.Mode.Production)
            {
                database = Databases.productionDatabase;
            }
            else
            {
                database = Databases.testDatabase;
            }
        }

        public void AddAppointment(string appointmentName, string visitor, DateTime value)
        {
            database.AddAppointment(new Appointment() { Name = appointmentName, Visitor = visitor, Date = value });
        }

        public List<Appointment> GetAllAppointments()
        {
            return database.GetAllAppointments();
        }

        public void RemoveAppointment(string name, string visitor, DateTime dateTime)
        {
            database.RemoveAppointment(new Appointment() { Name = name, Visitor = visitor, Date = dateTime });
        }
    }
}
