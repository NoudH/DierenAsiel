using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;

namespace DierenAsiel.Logic
{
    public interface ILogic
    {
        void AddAnimal(Animal animal);
        List<Animal> GetAllAnimals();
        void RemoveAnimal(Animal a);
        List<Animal> GetAnimalsOfType(Animal.Species type);
        Animal GetAnimalFromList(Animal.Species species, int index);
        DateTime GetUitlaatDate(Animal a);
        void SetUitlaatDate(Animal animal, Employee employee, DateTime date);
        bool Login(string username, string password);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Employee GetEmployeeByName(string name);
        void RemoveEmployee(Employee employee);
        List<Cage> GetAllCages();
        Cage GetCage(int cageNumber);
        void SetCleanDate(int cageNumber, DateTime value, string employee);
        void CreateUser(string username, string password);
    }
}
