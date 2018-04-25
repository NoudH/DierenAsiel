using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IDatabase
    {
        void AddAnimal(Animal animal);
        List<Animal> GetAllAnimals();
        void RemoveAnimal(Animal animal);
        DateTime GetUitlaatDate(Animal animal);
        void SetUitlaatDate(Animal animal, Employee employee, DateTime date);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Employee GetEmployeeByName(string name);
        void RemoveEmployee(Employee employee);
        void AddUser(string username, string hashedPassword);
        List<Cage> GetAllCages();
        DateTime GetCleaningdate(Cage cage);
        void SetCleanDate(int cageNumber, DateTime value, string employee);
        List<string> GetCharacteristicsFromAnimal(Animal animal);
        string GetUser(string username);
        List<DateTime> GetFeedingDates(Animal animal);
        void SetFeedingDate(Animal animal, DateTime value, Employee employee);
    }
}
