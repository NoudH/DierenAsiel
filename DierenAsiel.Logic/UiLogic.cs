using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DierenAsiel.Logic.Modes;

namespace DierenAsiel.Logic
{
    public class UiLogic : IUiLogic
    {
        private IAnimalLogic animalLogic;
        private IVisitorLogic visitorLogic;
        private IEmployeeLogic employeeLogic;
        private ICaretakingLogic caretakingLogic;

        public UiLogic(Mode mode)
        {
            if (mode == Mode.Production)
            {
                animalLogic = new AnimalLogicController(Mode.Production);
                visitorLogic = new VisitorLogic(Mode.Production);
                employeeLogic = new EmployeeLogicController(Mode.Production);
                caretakingLogic = new CaretakingLogicController(Mode.Production);
            }
            else
            {
                animalLogic = new AnimalLogicController(Mode.Test);
                visitorLogic = new VisitorLogic(Mode.Test);
                employeeLogic = new EmployeeLogicController(Mode.Test);
                caretakingLogic = new CaretakingLogicController(Mode.Test);
            }

        }

        public List<string> AppointmentsToday()
        {
            List<string> returnList = new List<string>();

            foreach (Appointment appointment in visitorLogic.GetAllAppointments())
            {
                if (appointment.Date.ToShortDateString() == DateTime.Today.ToShortDateString())
                {
                    returnList.Add($"Vandaag om {appointment.Date.ToShortTimeString()} staat er een afspraak gepland met {appointment.Visitor} genaamd: {appointment.Name}.");
                }
            }

            return returnList;
        }

        public List<string> TodoToday()
        {
            List<string> returnList = new List<string>();

            if (caretakingLogic.CalcDateWhenNoFoodLeft() <= DateTime.Today.AddDays(3))
            {
                returnList.Add($"WAARSCHUWING: Er is nog maar eten genoeg voor {(caretakingLogic.CalcDateWhenNoFoodLeft() - DateTime.Today).Days} dag(en)!");
            }

            foreach (Animal animal in animalLogic.GetAllAnimals())
            {
                if (caretakingLogic.GetFeedingDate(animal) <= DateTime.Today.AddDays(-1))
                {
                    returnList.Add($"Geef {animal.name} eten. (Laatst gevoerd op {caretakingLogic.GetFeedingDate(animal).ToShortDateString()})");
                }
                if (animal.species == Animal.Species.Dog && caretakingLogic.GetWalkingDate(animal) <= DateTime.Today.AddDays(-1))
                {
                    returnList.Add($"Laat {animal.name} uit. (Laatst uitgelaten op {caretakingLogic.GetWalkingDate(animal).ToShortDateString()})");
                }
            }

            foreach (Cage cage in caretakingLogic.GetAllCages())
            {
                if (cage.lastCleaningdate <= DateTime.Today.AddDays(-7))
                {
                    returnList.Add($"Maak hok {cage.cageNumber} schoon. (Laatst schoon gemaakt op {cage.lastCleaningdate.ToShortDateString()})");
                }
            }            

            return returnList;
        }
    }
}
