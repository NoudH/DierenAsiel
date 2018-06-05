using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface ICaretakingDatabase
    {
        /// <summary>
        /// Gets all the cages in the database.
        /// </summary>
        /// <returns></returns>
        List<Cage> GetAllCages();

        /// <summary>
        /// Gets the last cleaning date from the specified cage.
        /// </summary>
        /// <param name="cage"></param>
        /// <returns></returns>
        DateTime GetCleaningdate(Cage cage);

        /// <summary>
        /// Sets the cleaning date for the specified cage with the new date and name of employee.
        /// </summary>
        /// <param name="cageNumber"></param>
        /// <param name="value"></param>
        /// <param name="employee"></param>
        void SetCleanDate(int cageNumber, DateTime value, string employee);

        /// <summary>
        /// Gets the last date the specified animal was fed. 
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        List<DateTime> GetFeedingDates(Animal animal);

        /// <summary>
        /// Sets the date and employee when the specified animal was fed.
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="value"></param>
        /// <param name="employee"></param>
        void SetFeedingDate(Animal animal, DateTime value, Employee employee);

        /// <summary>
        /// Gets the last date of when the specified dog was walked.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        DateTime GetWalkingDate(Animal animal);

        /// <summary>
        /// Sets the date and employee of the specified dog when it was walked.
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="employee"></param>
        /// <param name="date"></param>
        void SetWalkingDate(Animal animal, Employee employee, DateTime date);

        /// <summary>
        /// Gets the foodportions of the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetFood(Enums.Foodtype type);

        /// <summary>
        /// Adds the specified amount of foodportions to the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        void AddFood(Enums.Foodtype type, int amount);
    }
}
