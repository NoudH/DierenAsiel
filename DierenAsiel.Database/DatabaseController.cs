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
    public class DatabaseController : IAnimalDatabase, ICaretakingDatabase, IEmployeeDatabase, IUserDatabase
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;

        #region helperFunctions
        private void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }
        #endregion

        public void AddAnimal(Animal animal)
        {
            string query = $"Insert into Dieren (Naam, Leeftijd, Gewicht, Geslacht, Afbeelding, Prijs, Soort, HokNummer, Gereserveerd) Values (@Name, @Age, @Weight, @Gender, @Image, @Price, @Species, @Cage, @Reserved)";
            SqlParameter[] parameters = 
            {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Image", animal.image),
                new SqlParameter("Price", animal.price),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage),
                new SqlParameter("Reserved", animal.reserved)
            };
            ExecuteNonQuery(query, parameters);
            foreach (string characteristic in animal.characteristics)
            {
                query = $"Insert into Eigenschappen (DierId, Eigenschap) values((select id from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender And Prijs = @Price And Soort = @Species And HokNummer = @Cage), @Characteristic)";
                SqlParameter[] Characterparameters =
                {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Price", animal.price),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage),
                new SqlParameter("Characteristic", characteristic)
                };
                ExecuteNonQuery(query, Characterparameters);
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
                            tempAnimal.reserved =reader.GetBoolean(8);
                            
                            returnList.Add(tempAnimal);
                        }
                    }
                    return returnList;
                }
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            string query = $"Delete from Eigenschappen where DierId = (select Id from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender And Prijs = @Price And Soort = @Species And HokNummer = @Cage)";
            SqlParameter[] CharacterParameters =
            {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Price", animal.price),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage),
            };
            ExecuteNonQuery(query, CharacterParameters);

            query = $"Delete from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender And Prijs = @Price And Soort = @Species And HokNummer = @Cage";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Price", animal.price),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage),
            };
            ExecuteNonQuery(query, parameters);
        }

        public DateTime GetUitlaatDate(Animal animal)
        {
            string query = $"Select Top 1 Uitlaten.Datum from Uitlaten, Dieren where Uitlaten.DierId = Dieren.Id AND Dieren.Naam = @Name AND Dieren.Leeftijd = @Age AND Dieren.Gewicht = @Weight AND Dieren.Geslacht = @Gender AND Dieren.Soort = @Species order by Uitlaten.Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("Name", animal.name),
                        new SqlParameter("Age", animal.age),
                        new SqlParameter("Weight", animal.weight),
                        new SqlParameter("Gender", animal.gender.ToString()),
                        new SqlParameter("Species", animal.species.ToString())
                    };
                    command.Parameters.AddRange(parameters);
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
            string query = $"insert into Uitlaten (DierId, Datum, VerzorgerId) Values((select Id from Dieren where Naam = @Name AND Soort = @Species AND Gewicht = @Weight AND Geslacht = @Gender), @Date, (select Id from Verzorgers where Naam = @EmployeeName))";
            SqlParameter[] parameters = {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Date", date.ToString("MM/dd/yyyy HH:mm")),
                new SqlParameter("EmployeeName", employee.name),
            };
            ExecuteNonQuery(query, parameters);
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
            string query = $"Insert into Verzorgers(Naam, Leeftijd, Gender, Adres, Telefoon) values(@Name, @Age, @Gender, @Address, @PhoneNumber)";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", employee.name),
                new SqlParameter("Age", employee.age),
                new SqlParameter("Gender", employee.gender.ToString()),
                new SqlParameter("Address", employee.address),
                new SqlParameter("PhoneNumber", employee.phoneNumber)
            };
            ExecuteNonQuery(query, parameters);
        }

        public Employee GetEmployeeByName(string name)
        {
            string query = $"Select * from Verzorgers where Naam = @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("Name", name)
                    };
                    command.Parameters.AddRange(parameters); 
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
            string query = $"Update Verzorgers Set Werkzaam = 0 where Naam = @Name And Leeftijd = @Age And Gender = @Gender And Adres = @Address And Telefoon = @PhoneNumber";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", employee.name),
                new SqlParameter("Age", employee.age),
                new SqlParameter("Gender", employee.gender.ToString()),
                new SqlParameter("Address", employee.address),
                new SqlParameter("PhoneNumber", employee.phoneNumber)
            };
            ExecuteNonQuery(query, parameters);
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
            string query = $"select top 1 Datum from HokVerschonen where HokId = @CageNumber order by Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("CageNumber", cage.cageNumber)
                    };
                    command.Parameters.AddRange(parameters);
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
            string query = $"insert into HokVerschonen(HokId, Datum, VerzorgerId) values(@CageNumber, @Date, (select top 1 Id from Verzorgers where Naam = @Name))";
            SqlParameter[] parameters =
            {
                new SqlParameter("CageNumber", cageNumber),
                new SqlParameter("Date", value.ToString("MM/dd/yyyy HH:mm")),
                new SqlParameter("Name", employee)
            };
            ExecuteNonQuery(query, parameters);
        }

        public List<string> GetCharacteristicsFromAnimal(Animal animal)
        {
            string query = $"select Eigenschap from Eigenschappen where DierId = (select id from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender and Soort = @Species and HokNummer = @Cage)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters = {
                        new SqlParameter("Name", animal.name),
                        new SqlParameter("Age", animal.age),
                        new SqlParameter("Weight", animal.weight),
                        new SqlParameter("Gender", animal.gender.ToString()),
                        new SqlParameter("Species", animal.species.ToString()),
                        new SqlParameter("Cage", animal.cage)
                    };
                    command.Parameters.AddRange(parameters);
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

        public string GetUserPassword(string username)
        {
            string query = $"select Wachtwoord from Gebruikers where GebruikersNaam = @Username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("Username", username)
                    };
                    command.Parameters.AddRange(parameters);
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
            string query = $"insert into Gebruikers(GebruikersNaam, Wachtwoord) values(@Username, @HashedPassword)";
            SqlParameter[] parameters =
            {
                new SqlParameter("Username", username),
                new SqlParameter("HashedPassword", hashedPassword)
            };
            ExecuteNonQuery(query, parameters);
        }

        public List<DateTime> GetFeedingDates(Animal animal)
        {
            string query = $"select Datum from Eten where DierId = (select id from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender and soort = @Species) Order by Datum desc";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters = {
                        new SqlParameter("Name", animal.name),
                        new SqlParameter("Age", animal.age),
                        new SqlParameter("Weight", animal.weight),
                        new SqlParameter("Gender", animal.gender.ToString()),
                        new SqlParameter("Species", animal.species.ToString())
                    };
                    command.Parameters.AddRange(parameters);
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
                $"(select id from Dieren where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender and Soort = @Species and HokNummer = @Cage)," +
                $"@Date," +
                $"(select id from Verzorgers where Naam = @EmployeeName and Leeftijd = @EmployeeAge and Gender = @EmployeeGender and Adres = @EmployeeAddress)" +
                $")";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage),
                new SqlParameter("Date", value.ToString("MM/dd/yyyy HH:mm")),
                new SqlParameter("EmployeeName", employee.name),
                new SqlParameter("EmployeeAge", employee.age),
                new SqlParameter("EmployeeGender", employee.gender.ToString()),
                new SqlParameter("EmployeeAddress", employee.address),
            };
            ExecuteNonQuery(query, parameters);
        }

        public void SetReserved(Animal animal)
        {
            string query = $"update Dieren Set Gereserveerd = @Reserved where Naam = @Name and Leeftijd = @Age and Gewicht = @Weight and Geslacht = @Gender And Prijs = @Price And Soort = @Species And HokNummer = @Cage";
            SqlParameter[] parameters = 
            {
                new SqlParameter("Reserved", animal.reserved),
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Price", animal.price),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage)
            };
            ExecuteNonQuery(query, parameters);
        }
    }
}
