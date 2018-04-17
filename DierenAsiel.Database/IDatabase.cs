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
    }
}
