using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;

namespace DierenAsiel.Logic
{
    public interface ICaretakingLogic
    {
        /// <summary>
        /// Gets the last date the specified dog was walked.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        DateTime GetWalkingDate(Animal a);

        /// <summary>
        /// Sets the walking date and employee of the specified dog.
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="employee"></param>
        /// <param name="date"></param>
        void SetWalkingDate(Animal animal, Employee employee, DateTime date);

        /// <summary>
        /// Returns all the cages found in the database.
        /// </summary>
        /// <returns></returns>
        List<Cage> GetAllCages();

        /// <summary>
        /// Returns the cage with the specified number.
        /// </summary>
        /// <param name="cageNumber"></param>
        /// <returns></returns>
        Cage GetCage(int cageNumber);

        /// <summary>
        /// Sets the cleaning date and employee of the specified cage.
        /// </summary>
        /// <param name="cageNumber"></param>
        /// <param name="value"></param>
        /// <param name="employee"></param>
        void SetCleanDate(int cageNumber, DateTime value, string employee);

        /// <summary>
        /// Gets the last feeding date of the specified animal.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        DateTime GetFeedingDate(Animal animal);

        /// <summary>
        /// Sets the feeding date and employee of the specified animal.
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="value"></param>
        /// <param name="employee"></param>
        void SetFeedingDate(Animal animal, DateTime value, Employee employee);

        /// <summary>
        /// Gets the amount of food of a specified foodtype.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetFood(Enums.Foodtype type);

        /// <summary>
        /// Adds the specified amount to the specified foodtype.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        void AddFood(Enums.Foodtype type, int amount);

        /// <summary>
        /// Returns the date of when there is no food left.
        /// </summary>
        /// <returns></returns>
        DateTime CalcDateWhenNoFoodLeft();
    }
   
}
