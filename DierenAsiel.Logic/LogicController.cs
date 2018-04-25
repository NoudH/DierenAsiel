using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;

namespace DierenAsiel.Logic
{
    public class LogicController : ILogic
    {
        IDatabase database = new DatabaseController();

        public void AddAnimal(Animal animal)
        {
            database.AddAnimal(animal);
        }

        public void AddEmployee(Employee employee)
        {
            database.AddEmployee(employee);
        }

        public void CreateUser(string username, string password)
        {
            database.AddUser(username, LoginAuthenticator.CreateUser(password));
        }

        public List<Animal> GetAllAnimals()
        {
            List<Animal> AllAnimals = database.GetAllAnimals();
            foreach (Animal animal in AllAnimals)
            {
                animal.characteristics = database.GetCharacteristicsFromAnimal(animal);
            }
            return AllAnimals;
        }

        public List<Cage> GetAllCages()
        {
            List<Cage> Cages = database.GetAllCages();
            List<Animal> Animals = database.GetAllAnimals();
            foreach (Cage cage in Cages)
            {
                cage.lastCleaningdate = database.GetCleaningdate(cage);
                cage.animals.AddRange(Animals.FindAll(x => x.cage == cage.cageNumber).ToList());
            }
            return Cages;
        }

        public List<Employee> GetAllEmployees()
        {
            return database.GetAllEmployees();
        }

        public Animal GetAnimalFromList(Animal.Species species, int index)
        {
            if (index != -1)
            {
                return GetAnimalsOfType(species)[index];
            }
            return new Animal();
        }

        public Animal GetAnimalFromList(int index)
        {
            return database.GetAllAnimals()[index];
        }

        public List<Animal> GetAnimalsOfType(Animal.Species type)
        {
            return database.GetAllAnimals().FindAll(x => x.species == type);
        }

        public Cage GetCage(int cageNumber)
        {
            return GetAllCages().Find(x => x.cageNumber == cageNumber);
        }

        public Employee GetEmployeeByName(string name)
        {
            return database.GetEmployeeByName(name);
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

        public bool Login(string username, string password)
        {
            string hashedPassword = database.GetUser(username);
            if (hashedPassword != null)
            {
                return LoginAuthenticator.LoginUser(password, database.GetUser(username));
            }
            return false;
        }

        public void RemoveAnimal(Animal animal)
        {
            database.RemoveAnimal(animal);
        }

        public void RemoveEmployee(Employee employee)
        {
            database.RemoveEmployee(employee);
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
