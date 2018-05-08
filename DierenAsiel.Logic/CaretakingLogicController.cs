using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;

namespace DierenAsiel.Logic
{
    public class CaretakingLogicController : ICaretakingLogic
    {
        ICaretakingDatabase database = new DatabaseController();
        IAnimalLogic AnimalLogic = new AnimalLogicController();

        public List<Cage> GetAllCages()
        {
            List<Cage> Cages = database.GetAllCages();
            List<Animal> Animals = AnimalLogic.GetAllAnimals();
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
