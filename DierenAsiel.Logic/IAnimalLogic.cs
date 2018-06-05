using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;

namespace DierenAsiel.Logic
{
    public interface IAnimalLogic
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
        void RemoveAnimal(Animal a);

        /// <summary>
        /// Gets all the animals of a specified specie.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Animal> GetAnimalsOfType(Animal.Species type);

        /// <summary>
        /// Gets the animal from the specified specie and index.
        /// </summary>
        /// <param name="species"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Animal GetAnimalFromList(Animal.Species species, int index);

        /// <summary>
        /// Gets the animal from the full list based on the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Animal GetAnimalFromList(int index);

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
