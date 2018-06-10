using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DierenAsielASP.Models;

namespace DierenAsielASP.Database
{
    interface IDatabase
    {
        /// <summary>
        /// Get all animals that are not reserved.
        /// </summary>
        /// <returns></returns>
        List<AnimalModel> GetAllAnimalsNotReserved();

        /// <summary>
        /// Get all animals.
        /// </summary>
        /// <returns></returns>
        List<AnimalModel> GetAllAnimals();

        /// <summary>
        /// Get Characteristics from a specified animal.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        List<string> GetCharacteristicsFromAnimal(AnimalModel animal);
    }
}
