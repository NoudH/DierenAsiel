using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DierenAsielASP.Models;

namespace DierenAsielASP.Database
{
    interface IDatabase
    {
        List<AnimalModel> GetAllAnimalsNotReserved();
        List<AnimalModel> GetAllAnimals();
        List<string> GetCharacteristicsFromAnimal(AnimalModel animal);
    }
}
