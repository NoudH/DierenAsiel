using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DierenAsiel;

namespace DierenAsielASP.Models
{
    public class AnimalModel
    {
        public AnimalModel()
        {

        }

        public enum Genders
        {
            Male = 0,
            Female = 1
        }

        public enum Species
        {
            Dog = 0,
            Cat = 1
        }

        public string name;
        public int age;
        public int weight;
        public Genders gender;
        public Species species;
        public string breed;
        public string image;
        public int cage;
        public bool reserved = false;
        public float price;
        public List<string> characteristics = new List<string>();

        internal AnimalModel FromAnimal(Animal animal)
        {
            name = animal.name;
            age = animal.age;
            weight = animal.weight;
            gender = (Genders)animal.gender;
            species = (Species)animal.species;
            breed = animal.breed;
            image = animal.image;
            cage = animal.cage;
            reserved = animal.reserved;
            price = animal.price;
            characteristics = animal.characteristics;

            return this;
        }

        public string about;

        public override string ToString()
        {
            return name;
        }
    }
}
