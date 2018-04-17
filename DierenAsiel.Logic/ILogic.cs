﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;

namespace DierenAsiel.Logic
{
    public interface ILogic
    {
        void AddAnimal(Animal animal);
        List<Animal> GetAllAnimals();
        void RemoveAnimal(Animal a);
        List<Animal> GetAnimalsOfType(Animal.Species type);
        Animal GetAnimalFromList(Animal.Species species, int index);
        DateTime GetUitlaatDate(Animal a);
        void SetUitlaatDate(Animal animal, Employee employee, DateTime date);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Employee GetEmployeeByName(string name);
    }
}