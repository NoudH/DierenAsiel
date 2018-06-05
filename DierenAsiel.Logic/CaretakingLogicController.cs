using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;
using System.Windows.Forms;
using static DierenAsiel.Logic.Modes;

namespace DierenAsiel.Logic
{
    public class CaretakingLogicController : ICaretakingLogic
    {
        ICaretakingDatabase database;
        IAnimalLogic animalLogic;

        public CaretakingLogicController(Mode mode)
        {
            if (mode == Mode.Production)
            {
                database = Databases.productionDatabase;
                animalLogic = new AnimalLogicController(Mode.Production);
            }
            else if (mode == Mode.Test)
            {
                database = Databases.testDatabase;
                animalLogic = new AnimalLogicController(Mode.Test);
            }
        }

        public void AddFood(Enums.Foodtype type, int amount)
        {
            database.AddFood(type, amount);
        }

        public DateTime CalcDateWhenNoFoodLeft()
        {
            List<Animal> animals = animalLogic.GetAllAnimals();
            decimal leastDaysLeft = -1;
            foreach (Enums.Foodtype type in Enum.GetValues(typeof(Enums.Foodtype)))
            {
                decimal daysLeft = (GetFood(type) / animals.Where(x => (int)x.species == (int)type).ToList().Count);
                if (daysLeft < leastDaysLeft || leastDaysLeft == -1)
                {
                    leastDaysLeft = daysLeft;
                }
            }
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + (int)Math.Floor(leastDaysLeft));            
            return date;
        }

        public List<Cage> GetAllCages()
        {
            List<Cage> Cages = database.GetAllCages();
            List<Animal> Animals = animalLogic.GetAllAnimals();
            foreach (Cage cage in Cages)
            {
                cage.lastCleaningdate = database.GetCleaningdate(cage);
                cage.animals.AddRange(Animals.FindAll(x => x.cage == cage.cageNumber).ToList());
            }
            return Cages;
        }

        public Cage GetCage(int cageNumber)
        {
            return GetAllCages().Find(x => x.cageNumber == cageNumber);
        }

        public DateTime GetFeedingDate(Animal animal)
        {
            return database.GetFeedingDates(animal).First();
        }

        public int GetFood(Enums.Foodtype type)
        {
            return database.GetFood(type);
        }

        public DateTime GetUitlaatDate(Animal animal)
        {
            if (animal.species != Animal.Species.Dog)
            {
                throw new Exception();
            }
            else
            {
                return database.GetUitlaatDate(animal);
            }
        }

        public void SetCleanDate(int cageNumber, DateTime value, string employee)
        {
            database.SetCleanDate(cageNumber, value, employee);
        }

        public void SetFeedingDate(Animal animal, DateTime value, Employee employee)
        {
            database.SetFeedingDate(animal, value, employee);
        }

        public void SetUitlaatDate(Animal animal, Employee employee, DateTime date)
        {
            database.SetUitlaatDate(animal, employee, date);
        }
    }
}
