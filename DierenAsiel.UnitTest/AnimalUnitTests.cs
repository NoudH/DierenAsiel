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
            employeeLogic.RemoveEmployee(testEmployee);

            Assert.IsFalse(database.GetAllEmployees().Contains(testEmployee));
        }

        [TestMethod()]
        public void LoginAndCreateUser()
        {
            authenticationLogic.CreateUser("Kees", "MyPassword123");

            Assert.IsTrue(authenticationLogic.Login("Kees", "MyPassword123"));
        }
    }
}
