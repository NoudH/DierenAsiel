using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel.Database;

namespace DierenAsiel.Logic
{
    public class AnimalLogicController : IAnimalLogic
    {
        IAnimalDatabase database = new DatabaseController();

        public void AddAnimal(Animal animal)
        {
            database.AddAnimal(animal);
        }        

        public List<Animal> GetAllAnimals()
        {
            List<Animal> AllAnimals = database.GetAllAnimals();
            foreach (Animal animal in AllAnimals)
            {
                animal.characteristics = database.GetCharacteristicsFromAnimal(animal);
            }
            return AllAnimals;
        }

        public Animal GetAnimalFromList(Animal.Species species, int index)
        {
            if (index != -1)
            {
                return GetAnimalsOfType(species)[index];
            }
            return new Animal();
        }

        public Animal GetAnimalFromList(int index)
        {
            return database.GetAllAnimals()[index];
        }

        public List<Animal> GetAnimalsOfType(Animal.Species type)
        {
            return database.GetAllAnimals().FindAll(x => x.species == type);
        }
        
        public void RemoveAnimal(Animal animal)
        {
            database.RemoveAnimal(animal);
        }

        public void SetReserved(Animal animal)
        {
            database.SetReserved(animal);
        }
    }
}
