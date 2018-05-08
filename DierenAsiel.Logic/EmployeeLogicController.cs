using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;

namespace DierenAsiel.Logic
{
    public class EmployeeLogicController : IEmployeeLogic
    {
        IEmployeeDatabase database = new DatabaseController();

        public void AddEmployee(Employee employee)
        {
            database.AddEmployee(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return database.GetAllEmployees();
        }

        public Employee GetEmployeeByName(string name)
        {
            return database.GetEmployeeByName(name);
        }

        public void RemoveEmployee(Employee employee)
        {
            database.RemoveEmployee(employee);
        }
    }
}
