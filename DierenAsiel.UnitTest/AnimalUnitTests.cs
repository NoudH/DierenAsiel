using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using DierenAsiel;
using DierenAsiel.UI;
using DierenAsiel.Logic;
using DierenAsiel.Database;
using static DierenAsiel.Logic.Modes;
using System.Collections.Generic;

namespace DierenAsiel.UnitTest
{
    [TestClass()]
    public class AnimalUnitTests
    {
        IAnimalLogic animalLogic = new AnimalLogicController(Mode.Test);
        IEmployeeLogic employeeLogic = new EmployeeLogicController(Mode.Test);
        ICaretakingLogic caretakingLogic = new CaretakingLogicController(Mode.Test);
        IAuthenticationLogic authenticationLogic = new LoginAuthenticator(Mode.Test);
        IVisitorLogic visitorLogic = new VisitorLogic(Mode.Test);
        ITodoLogic todoLogic = new TodoLogic(Mode.Test);

        TestDatabaseController database = Databases.testDatabase;

        #region TestClasses
        Animal testAnimal = new Animal()
        {
            name = "Pluisje",
            age = 3,
            weight = 10,
            species = Animal.Species.Cat,
            gender = Animal.Genders.Female,
            cage = 0,
            breed = "Britse Langhaar",
            characteristics = new List<string>() { "Aardig", "Zacht", "Druk" },
            image = "Base64 string",
            price = 120.50f,
            about = "Afgestaan door het oude baasje.",
            reserved = false
        };

        Animal testAnimal1 = new Animal()
        {
            name = "Keesje",
            age = 3,
            weight = 10,
            species = Animal.Species.Cat,
            gender = Animal.Genders.Male,
            cage = 0,
            breed = "Britse Langhaar",
            characteristics = new List<string>() { "Aardig", "Zacht", "Druk" },
            image = "Base64 string",
            price = 120.50f,
            about = "Afgestaan door het oude baasje.",
            reserved = false
        };

        Animal testAnimal2 = new Animal()
        {
            name = "Rufus",
            age = 3,
            weight = 10,
            species = Animal.Species.Dog,
            gender = Animal.Genders.Female,
            cage = 0,
            breed = "Britse Langhaar",
            characteristics = new List<string>() { "Aardig", "Zacht", "Druk" },
            image = "Base64 string",
            price = 120.50f,
            about = "Afgestaan door het oude baasje.",
            reserved = false
        };

        Employee testEmployee = new Employee()
        {
            name = "Kees Hermans",
            age = 25,
            gender = Employee.Gender.Male,
            address = "Berkenlaan 8, Zeeland",
            phoneNumber = "0645784545"
        };
        #endregion

        [TestMethod()]
        public void AddAnimal()
        {            
            animalLogic.AddAnimal(testAnimal);

            Assert.IsNotNull(database.GetAllAnimals().Find(x => x == testAnimal));
        }

        [TestMethod()]
        public void RemoveAnimal()
        {
            animalLogic.AddAnimal(testAnimal);
            if (!database.GetAllAnimals().Contains(testAnimal))
            {
                Assert.Fail();
            }
            animalLogic.RemoveAnimal(testAnimal);

            Assert.IsFalse(database.GetAllAnimals().Contains(testAnimal));
        }

        [TestMethod()]
        public void AddEmployee()
        {
            employeeLogic.AddEmployee(testEmployee);

            Assert.IsTrue(database.GetAllEmployees().Contains(testEmployee));
        }

        [TestMethod()]
        public void RemoveEmployee()
        {
            employeeLogic.AddEmployee(testEmployee);
            if (!database.GetAllEmployees().Contains(testEmployee))
            {
                Assert.Fail();
            }

            employeeLogic.RemoveEmployee(testEmployee);

            Assert.IsFalse(database.GetAllEmployees().Contains(testEmployee));
        }

        [TestMethod()]
        public void LoginAndCreateUser()
        {
            authenticationLogic.CreateUser("Kees", "MyPassword123");

            Assert.IsTrue(authenticationLogic.Login("Kees", "MyPassword123"));
        }

        [TestMethod()]
        public void AddFoodOfDifferentKinds()
        {
            caretakingLogic.AddFood(Enums.Foodtype.Dogfood, 4);
            caretakingLogic.AddFood(Enums.Foodtype.Catfood, 6);
            Assert.IsTrue(database.FoodCount.Where(x => x.foodtype == Enums.Foodtype.Dogfood).Select(x => x.Amount).First() == 4 && database.FoodCount.Where(x => x.foodtype == Enums.Foodtype.Catfood).Select(x => x.Amount).First() == 6);
        }

        [TestMethod()]
        public void CalcDaysLeftOfFood()
        {
            database.FoodCount.ForEach(x => x.Amount = 0);
            database.Animals.Clear();

            database.Animals.AddRange(new Animal[] { testAnimal, testAnimal1, testAnimal2 });
            caretakingLogic.AddFood(Enums.Foodtype.Dogfood, 4);
            caretakingLogic.AddFood(Enums.Foodtype.Catfood, 6);

            Console.WriteLine(caretakingLogic.CalcDateWhenNoFoodLeft());

            Assert.IsTrue(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 3) == caretakingLogic.CalcDateWhenNoFoodLeft());
        }

        [TestMethod()]
        public void RemoveAppointment()
        {
            Appointment appointment = new Appointment() { Name = "TestAppointment", Visitor = "TestVisitor", Date = DateTime.Today };
            visitorLogic.AddAppointment(appointment.Name, appointment.Visitor, appointment.Date);
            if (database.Appointments.Find(x => x.Name == appointment.Name && x.Visitor == appointment.Visitor && x.Date == appointment.Date) == null)
            {
                Assert.Fail();
            }
            visitorLogic.RemoveAppointment(appointment.Name, appointment.Visitor, appointment.Date);
            Console.WriteLine(database.Appointments.Find(x => x.Name == appointment.Name && x.Visitor == appointment.Visitor && x.Date == appointment.Date));
            Assert.IsTrue(database.Appointments.Find(x => x.Name == appointment.Name && x.Visitor == appointment.Visitor && x.Date == appointment.Date) == null);
        }

        [TestMethod()]
        public void AddAppointment()
        {
            Appointment appointment = new Appointment() { Name = "Test", Visitor = "Test", Date = DateTime.Today };
            visitorLogic.AddAppointment(appointment.Name, appointment.Visitor, appointment.Date);
            Assert.IsTrue(database.Appointments.Find(x => x.Name == appointment.Name && x.Visitor == appointment.Visitor && x.Date == appointment.Date) != null);
        }

        [TestMethod]
        public void AppointmentsToday()
        {
            database.Appointments.Clear();

            visitorLogic.AddAppointment("Test123", "Test321", DateTime.Today);
            visitorLogic.AddAppointment("Test126", "Test621", DateTime.Today);
            visitorLogic.AddAppointment("Test124", "Test421", DateTime.Today.AddDays(1));
            visitorLogic.AddAppointment("Test127", "Test721", DateTime.Today.AddDays(-1));

            Assert.IsTrue(todoLogic.AppointmentsToday().Count() == 2);
        }
    }
}
