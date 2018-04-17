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

        public List<Animal> GetAllAnimals()
        {
            return database.GetAllAnimals();
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

        public List<Animal> GetAnimalsOfType(Animal.Species type)
        {
            return database.GetAllAnimals().FindAll(x => x.species == type);
        }

        public Employee GetEmployeeByName(string name)
        {
            return database.GetEmployeeByName(name);
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

        public void RemoveAnimal(Animal animal)
        {
            database.RemoveAnimal(animal);
        }

        public void SetUitlaatDate(Animal animal, Employee employee, DateTime date)
        {
            database.SetUitlaatDate(animal, employee, date);
        }
    }
}
