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
        void AddAnimal(Animal animal);
        List<Animal> GetAllAnimals();
        void RemoveAnimal(Animal a);
        List<Animal> GetAnimalsOfType(Animal.Species type);
        Animal GetAnimalFromList(Animal.Species species, int index);
        Animal GetAnimalFromList(int index);
        void SetReserved(Animal animal);
        void EditAnimal(Animal oldAnimal, Animal newAnimal);
    }    
}
