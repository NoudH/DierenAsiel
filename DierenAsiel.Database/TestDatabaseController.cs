using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DierenAsiel.Database
{
    public class TestDatabaseController : IAnimalDatabase, IEmployeeDatabase, ICaretakingDatabase, IUserDatabase
    {
        public struct User
        {
            public string username;
            public string password;
        }

        public struct CleaningDate
        {
            public int cageId;
            public DateTime date;
            public Employee employee;
        }

        public struct WalkingDate
        {
            public Animal animal;
            public DateTime date;
            public Employee employee;
        }

        public struct FeedingDate
        {
            public Animal animal;
            public DateTime date;
            public Employee employee;
        }

        public struct Food
        {
            public Enums.Foodtype foodtype;
            public int Amount;
        }

        public List<Animal> Animals = new List<Animal>();
        public List<Employee> Employees = new List<Employee>();
        public List<User> Users = new List<User>();
        public List<Cage> Cages = new List<Cage>();
        public List<CleaningDate> CleaningDates = new List<CleaningDate>();
        public List<WalkingDate> WalkingDates = new List<WalkingDate>();
        public List<FeedingDate> FeedingDates = new List<FeedingDate>();
        public List<Food> FoodCount = new List<Food>();

        public TestDatabaseController()
        {
            for (int i = 0; i < 1000; i++)
            {
                Cages.Add(new Cage() { cageNumber = i });
            }
            foreach (Enums.Foodtype item in Enum.GetValues(typeof(Enums.Foodtype)))
            {
                FoodCount.Add(new Food() { foodtype = item, Amount = 0 });
            }
        }

        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public void AddUser(string username, string hashedPassword)
        {
            Users.Add(new User() { username = username, password = hashedPassword });
        }

        public List<Animal> GetAllAnimals()
        {
            return Animals;
        }

        public List<Cage> GetAllCages()
        {
            return Cages;
        }

        public List<Employee> GetAllEmployees()
        {
            return Employees;
        }

        public List<string> GetCharacteristicsFromAnimal(Animal animal)
        {
            return Animals.Find(x => x == animal).characteristics;
        }

        public DateTime GetCleaningdate(Cage cage)
        {
            return CleaningDates.FindAll(x => x.cageId == cage.cageNumber).Select(x => x.date).Max();
        }

        public Employee GetEmployeeByName(string name)
        {
            return Employees.Find(x => x.name == name);
        }

        public List<DateTime> GetFeedingDates(Animal animal)
        {
            return FeedingDates.FindAll(x => x.animal == animal).Select(y => y.date).ToList();
        }

        public DateTime GetUitlaatDate(Animal animal)
        {
            return WalkingDates.FindAll(x => x.animal == animal).Select(y => y.date).Max();
        }

        public string GetUserPassword(string username)
        {
            return Users.Find(x => x.username == username).password;
        }

        public void RemoveAnimal(Animal animal)
        {
            Animals.Remove(animal);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }

        public void SetCleanDate(int cageNumber, DateTime value, string employee)
        {
            CleaningDates.Add(new CleaningDate() { cageId = cageNumber, date = value, employee = GetEmployeeByName(employee) });
        }

        public void SetFeedingDate(Animal animal, DateTime value, Employee employee)
        {
            FeedingDates.Add(new FeedingDate() { animal = animal, date = value, employee = employee });
        }

        public void SetReserved(Animal animal)
        {
            Animals.Find(x => x.name == animal.name && x.price == animal.price && x.age == animal.age && x.weight == animal.weight);
        }

        public void SetUitlaatDate(Animal animal, Employee employee, DateTime date)
        {
            WalkingDates.Add(new WalkingDate() { animal = animal, employee = employee, date = date });
        }

        public void EditAnimal(Animal oldAnimal, Animal newAnimal)
        {
            Animals[Animals.FindIndex(x => x == oldAnimal)] = newAnimal;
        }

        public int GetFood(Enums.Foodtype type)
        {
            return FoodCount.Where(x => x.foodtype == type).Select(x => x.Amount).First();
        }

        public void AddFood(Enums.Foodtype type, int amount)
        {
            FoodCount[FoodCount.FindIndex(x => x.foodtype == type)] = new Food() { foodtype = type, Amount = amount};
        }
    }
}
