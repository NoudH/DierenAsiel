using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IAnimalDatabase
    {
        /// <summary>
        /// Adds the animal to the database.
        /// </summary>
        /// <param name="animal"></param>
        void AddAnimal(Animal animal);

        /// <summary>
        /// Gets all the animals currently in the database.
        /// </summary>
        /// <returns></returns>
        List<Animal> GetAllAnimals();

        /// <summary>
        /// Removes the specified animal from the database.
        /// </summary>
        /// <param name="animal"></param>
        void RemoveAnimal(Animal animal);

        /// <summary>
        /// Gets all the characteristics from the specified animal.
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        List<string> GetCharacteristicsFromAnimal(Animal animal);

        /// <summary>
        /// Sets the reserved property of the specified animal in the database.
        /// </summary>
        /// <param name="animal"></param>
        void SetReserved(Animal animal);

        /// <summary>
        /// Replaces the specified animal with the new one (used for edditing). 
        /// </summary>
        /// <param name="oldAnimal"></param>
        /// <param name="newAnimal"></param>
        void EditAnimal(Animal oldAnimal, Animal newAnimal);
    }
}
