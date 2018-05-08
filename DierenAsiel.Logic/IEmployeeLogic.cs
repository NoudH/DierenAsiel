using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Logic
{
    public interface IEmployeeLogic
    {
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Employee GetEmployeeByName(string name);
        void RemoveEmployee(Employee employee);
    }
}
