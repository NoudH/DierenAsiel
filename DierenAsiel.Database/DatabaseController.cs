using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DierenAsiel;
using System.Data.SqlClient;
using System.Configuration;

namespace DierenAsiel.Database
{
    public class DatabaseController : IDatabase
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;

        #region helperFunctions
        private void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        //private SqlDataReader ExecuteReader(string query)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            SqlDataReader reader = command.ExecuteReader();
        //            return reader;
        //        }
        //    }
        //}
        #endregion

        public void AddAnimal(Animal animal)
        {
            string query = $"Insert into Dieren (Naam, Leeftijd, Gewicht, Geslacht, Afbeelding, Prijs, Soort, HokNummer, Gereserveerd) Values ('{animal.name}', {animal.age}, {animal.weight}, '{animal.gender.ToString()}', '{animal.image}', {animal.price}, '{animal.species.ToString()}', {animal.cage}, {Convert.ToInt16(animal.reserved)})";
            ExecuteNonQuery(query);
            foreach (string characteristic in animal.characteristics)
            {
                query = $"Insert into Eigenschappen (DierId, Eigenschap) values((select id from Dieren where Naam = '{animal.name}' and Leeftijd = {animal.age} And Gewicht = '{animal.weight}' And Geslacht = '{animal.gender.ToString()}' And Soort = '{animal.species.ToString()}' And HokNummer = {animal.cage}), '{characteristic}')";
                ExecuteNonQuery(query);
            }            
        }

        public List<Animal> GetAllAnimals()
        {
            string query = "select * from dieren";
            List<Animal> returnList = new List<Animal>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader() )
                    {
                        while (reader.Read())
                        {
                            Animal tempAnimal = new Animal();
                            tempAnimal.name = reader.GetString(1);
                            tempAnimal.age = reader.GetInt32(2);
                            tempAnimal.weight = reader.GetInt32(3);
                            tempAnimal.gender = (Animal.Genders)Enum.Parse(typeof(Animal.Genders), reader.GetString(4));
                            tempAnimal.price = (float)reader.GetDouble(5); //Blame microsoft
                            tempAnimal.species = (Animal.Species)Enum.Parse(typeof(Animal.Species), reader.GetString(6));
                            tempAnimal.cage = reader.GetInt32(7);
                            tempAnimal.reserved = Convert.ToBoolean(reader.GetByte(8));
                            
                            returnList.Add(tempAnimal);
                        }
                    }
                    return returnList;
                }
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            string query = $"Delete from Eigenschappen where DierId = (select Id from Dieren where Naam = '{animal.name}' AND Leeftijd = {animal.age} AND Gewicht = {animal.weight} AND Prijs = {animal.price} AND Soort = '{animal.species.ToString()}' and HokNummer = {animal.cage})";
            ExecuteNonQuery(query);

            query = $"Delete from Dieren where Naam = '{animal.name}' AND Leeftijd = {animal.age} AND Gewicht = {animal.weight} AND Prijs = {animal.price} AND Soort = '{animal.species.ToString()}' and HokNummer = {animal.cage}";
            ExecuteNonQuery(query);
        }

        public DateTime GetUitlaatDate(Animal animal)
        {
            string query = $"Select Top 1 Uitlaten.Datum from Uitlaten, Dieren where Uitlaten.DierId = Dieren.Id AND Dieren.Naam = '{animal.name}' AND Dieren.Leeftijd = {animal.age} AND Dieren.Gewicht = {animal.weight} AND Dieren.Geslacht = '{animal.gender.ToString()}' AND Dieren.Soort = '{animal.species.ToString()}' order by Uitlaten.Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDateTime(0);
                        }
                        return new DateTime(1753, 1, 1);
                    }
                }
            }
        }

        public void SetUitlaatDate(Animal animal, Employee employee, DateTime date)
        {            
            string query = $"insert into Uitlaten (DierId, Datum, VerzorgerId) Values((select Id from Dieren where Naam = '{animal.name}' AND Soort = '{animal.species.ToString()}' AND Gewicht = {animal.weight} AND Geslacht = '{animal.gender.ToString()}'), '{date.ToString("MM/dd/yyyy HH:mm")}', (select Id from Verzorgers where Naam = '{employee.name}'))";
            ExecuteNonQuery(query);
        }

        public List<Employee> GetAllEmployees()
        {
            string query = "select * from Verzorgers where Werkzaam = 1";
            List<Employee> returnList = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee tempEmployee = new Employee();
                            tempEmployee.name = reader.GetString(1);
                            tempEmployee.age = reader.GetInt32(2);
                            tempEmployee.gender = (Employee.Gender)Enum.Parse(typeof(Employee.Gender), reader.GetString(3));
                            tempEmployee.address = reader.GetString(4);
                            tempEmployee.phoneNumber = reader.GetString(5);

                            returnList.Add(tempEmployee);
                        }
                    }
                    return returnList;
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            string query = $"Insert into Verzorgers(Naam, Leeftijd, Gender, Adres, Telefoon) values('{employee.name}', {employee.age}, '{employee.gender.ToString()}', '{employee.address}', '{employee.phoneNumber}')";
            ExecuteNonQuery(query);
        }

        public Employee GetEmployeeByName(string name)
        {
            string query = $"Select * from Verzorgers where Naam = '{name}'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Employee()
                            {
                                name = reader.GetString(1),
                                age = reader.GetInt32(2),
                                gender = (Employee.Gender)Enum.Parse(typeof(Employee.Gender), reader.GetString(3)),
                                address = reader.GetString(4),
                                phoneNumber = reader.GetString(5)
                            };
                        }
                        throw new Exception();
                    }
                }
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            string query = $"Update Verzorgers Set Werkzaam = 0 where Naam = '{employee.name}' And Leeftijd = {employee.age} And Gender = '{employee.gender.ToString()}' And Adres = '{employee.address}'";
            ExecuteNonQuery(query);
        }

        public List<Cage> GetAllCages()
        {
            List<Cage> Cages = new List<Cage>();
            string query = $"Select distinct HokNummer from Dieren";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {                
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cage C = new Cage() { cageNumber = reader.GetInt32(0) };
                            Cages.Add(C);
                        }
                    }
                }
            }
            return Cages;
        }

        public DateTime GetCleaningdate(Cage cage)
        {
            string query = $"select top 1 Datum from HokVerschonen where HokId = {cage.cageNumber} order by Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetDateTime(0);
                        }
                        return new DateTime(1753, 1, 1);
                    }
                }
            }
        }

        public void SetCleanDate(int cageNumber, DateTime value, string employee)
        {
            string query = $"insert into HokVerschonen(HokId, Datum, VerzorgerId) values({cageNumber}, '{value.ToString("MM/dd/yyyy HH:mm")}', (select top 1 Id from Verzorgers where Naam = '{employee}'))";
            ExecuteNonQuery(query);
        }

        public List<string> GetCharacteristicsFromAnimal(Animal animal)
        {
            string query = $"select Eigenschap from Eigenschappen where DierId = (select id from Dieren where Naam = '{animal.name}' and Leeftijd = {animal.age} and Gewicht = {animal.weight} and Geslacht = '{animal.gender.ToString()}' and Soort = '{animal.species.ToString()} and HokNummer = {animal.cage}')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> characteristics = new List<string>();
                        while (reader.Read())
                        {
                            characteristics.Add(reader.GetString(0));
                        }
                        return characteristics;
                    }
                }
            }
        }

        public string GetUser(string username)
        {
            string query = $"select Wachtwoord from Gebruikers where GebruikersNaam = '{username}'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                        return null;
                    }
                }
            }
        }

        public void AddUser(string username, string hashedPassword)
        {
            string query = $"insert into Gebruikers(GebruikersNaam, Wachtwoord) values('{username}', '{hashedPassword}')";
            ExecuteNonQuery(query);
        }

        public List<DateTime> GetFeedingDates(Animal animal)
        {
            string query = $"select Datum from Eten where DierId = (select id from Dieren where Naam = '{animal.name}' and Leeftijd = {animal.age} and Gewicht = {animal.weight} and Geslacht = '{animal.gender.ToString()}' and soort = '{animal.species.ToString()}') Order by Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<DateTime> FeedingDates = new List<DateTime>();
                        while (reader.Read())
                        {
                            FeedingDates.Add(reader.GetDateTime(0));
                        }
                        FeedingDates.Add(new DateTime(1753, 1, 1));
                        return FeedingDates;
                    }
                }
            }
        }

        public void SetFeedingDate(Animal animal, DateTime value, Employee employee)
        {
            string query = $"insert into Eten (DierId, Datum, VerzorgerId) values (" +
                $"(select id from Dieren where Naam = '{animal.name}' and Leeftijd = {animal.age} and Gewicht = {animal.weight} and Geslacht = '{animal.gender.ToString()}' and Soort = '{animal.species.ToString()}' and HokNummer = {animal.cage})," +
                $"'{value.ToString("MM/dd/yyyy HH:mm")}'," +
                $"(select id from Verzorgers where Naam = '{employee.name}' and Leeftijd = {employee.age} and Gender = '{employee.gender.ToString()}' and Adres = '{employee.address}')" +
                $")";
            ExecuteNonQuery(query);
        }
    }
}
