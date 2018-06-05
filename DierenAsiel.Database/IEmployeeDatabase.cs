using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IEmployeeDatabase
    {
        /// <summary>
        /// Gets a list of all employees currently in the database.
        /// </summary>
        /// <returns></returns>
        List<Employee> GetAllEmployees();

        /// <summary>
        /// Adds the specified employee to the database.
        /// </summary>
        /// <param name="employee"></param>
        void AddEmployee(Employee employee);

        /// <summary>
        /// Returns the employee object based on the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Employee GetEmployeeByName(string name);

        /// <summary>
        /// Removes the specified employee from the database.
        /// </summary>
        /// <param name="employee"></param>
        void RemoveEmployee(Employee employee);
    }
}
