using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public interface IAnimalDatabase
    {
        void AddAnimal(Animal animal);
        List<Animal> GetAllAnimals();
        void RemoveAnimal(Animal animal);
        List<string> GetCharacteristicsFromAnimal(Animal animal);        
    }
}
