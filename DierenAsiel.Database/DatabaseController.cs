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
        public string ConnectionString { get =>  ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString; }

        #region helperFunctions
        private void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"Insert into Animals (Name, Age, Weight, Gender, Image, Price, Species, Cage, Reserved, Breed, About) Values (@Name, @Age, @Weight, @Gender, @Image, @Price, @Species, @Cage, @Reserved, @Breed, @About)";
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
                new SqlParameter("Reserved", animal.reserved),
                new SqlParameter("Breed", animal.breed),
                new SqlParameter("About", animal.about)
            };
            ExecuteNonQuery(query, parameters);
            foreach (string characteristic in animal.characteristics)
            {
                query = $"Insert into Characteristics (AnimalId, Characteristic) values((select id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender And Price = @Price And Species = @Species And Cage = @Cage), @Characteristic)";
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
            string query = "select * from Animals";
            List<Animal> returnList = new List<Animal>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                            tempAnimal.breed = reader.GetString(10);
                            tempAnimal.cage = reader.GetInt32(7);
                            tempAnimal.reserved = reader.GetBoolean(8);
                            tempAnimal.image = reader.GetString(9);
                            tempAnimal.about = reader.GetString(11);

                            returnList.Add(tempAnimal);
                        }
                    }
                    return returnList;
                }
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            string query = $"Delete from Characteristics where AnimalId = (select Id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender And Price = @Price And Species = @Species And Cage = @Cage)";
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

            query = $"Delete from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender And Price = @Price And Species = @Species And Cage = @Cage";
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
            string query = $"Select Top 1 Walking.Date from Walking, Animals where Walking.AnimalId = Animals.Id AND Animals.Name = @Name AND Animals.Age = @Age AND Animals.Weight = @Weight AND Animals.Gender = @Gender AND Animals.Species = @Species order by Walking.Date desc";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"insert into Walking (AnimalId, Date, EmployeeId) Values((select Id from Animals where Name = @Name AND Species = @Species AND Weight = @Weight AND Gender = @Gender), @Date, (select Id from Employees where Name = @EmployeeName))";
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
            string query = "select * from Employees where Active = 1";
            List<Employee> returnList = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"Insert into Employees(Name, Age, Gender, Address, Phone) values(@Name, @Age, @Gender, @Address, @PhoneNumber)";
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
            string query = $"Select * from Employees where Name = @Name";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"Update Employees Set Active = 0 where Name = @Name And Age = @Age And Gender = @Gender And Address = @Address And Phone = @PhoneNumber";
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
            string query = $"Select distinct Cage from Animals";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"select top 1 Date from Cleaning where CageId = @CageNumber order by Date desc";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"insert into Cleaning(CageId, Date, EmployeeId) values(@CageNumber, @Date, (select top 1 Id from Employees where Name = @Name))";
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
            string query = $"select Characteristic from Characteristics where AnimalId = (select id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender and Species = @Species and Cage = @Cage)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"select Password from Users where UserName = @Username";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"insert into User(UserName, Password) values(@Username, @HashedPassword)";
            SqlParameter[] parameters =
            {
                new SqlParameter("Username", username),
                new SqlParameter("HashedPassword", hashedPassword)
            };
            ExecuteNonQuery(query, parameters);
        }

        public List<DateTime> GetFeedingDates(Animal animal)
        {
            string query = $"select Date from Feeding where AnimalId = (select id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender and Species = @Species) Order by Date desc";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
            string query = $"insert into Feeding (AnimalId, Date, EmployeeId) values (" +
                $"(select id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender and Species = @Species and Cage = @Cage)," +
                $"@Date," +
                $"(select id from Employees where Name = @EmployeeName and Age = @EmployeeAge and Gender = @EmployeeGender and Address = @EmployeeAddress)" +
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
            string query = $"update Animals Set Reserved = @Reserved where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender And Price = @Price And Species = @Species And Cage = @Cage";
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

        public void EditAnimal(Animal oldAnimal, Animal newAnimal)
        {
            string query = "update Animals set Name = @Name, Age = @Age, Weight = @Weight, Gender = @Gender, Price = @Price, Species = @Species, Cage = @Cage, Reserved = @Reserved, Image = @Image, Breed = @Breed, About = @About WHERE Name = @oldName and Age = @oldAge and Weight = @oldWeight and Gender = @oldGender and Price = @oldPrice and Species = @oldSpecies and Cage = @oldCage and Reserved = @oldReserved and Image = @oldImage and Breed = @oldBreed and About = @oldAbout";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", newAnimal.name),
                new SqlParameter("Age", newAnimal.age),
                new SqlParameter("Weight", newAnimal.weight),
                new SqlParameter("Gender", newAnimal.gender.ToString()),
                new SqlParameter("Price", newAnimal.price),
                new SqlParameter("Species", newAnimal.species.ToString()),
                new SqlParameter("Cage", newAnimal.cage),
                new SqlParameter("Reserved", newAnimal.reserved),
                new SqlParameter("Image", newAnimal.image),
                new SqlParameter("Breed", newAnimal.breed),
                new SqlParameter("About", newAnimal.about),

                new SqlParameter("oldName", oldAnimal.name),
                new SqlParameter("oldAge", oldAnimal.age),
                new SqlParameter("oldWeight", oldAnimal.weight),
                new SqlParameter("oldGender", oldAnimal.gender.ToString()),
                new SqlParameter("oldPrice", oldAnimal.price),
                new SqlParameter("oldSpecies", oldAnimal.species.ToString()),
                new SqlParameter("oldCage", oldAnimal.cage),
                new SqlParameter("oldReserved", oldAnimal.reserved),
                new SqlParameter("oldImage", oldAnimal.image),
                new SqlParameter("oldBreed", oldAnimal.breed),
                new SqlParameter("oldAbout", oldAnimal.about),

            };
            ExecuteNonQuery(query, parameters);
        }

        public int GetFood(Enums.Foodtype type)
        {
            string query = "Select Amount from Food where Foodtype = @Foodtype";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {               
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("Foodtype", type.ToString()));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        return 0;
                    }
                }
            }
        }

        public void AddFood(Enums.Foodtype type, int amount)
        {
            string query = "Update Food set Amount = @Amount where Foodtype = @Foodtype";
            SqlParameter[] parameters =
            {
                new SqlParameter("Foodtype", type.ToString()),
                new SqlParameter("Amount", amount)
            };
            ExecuteNonQuery(query, parameters);
        }
    }
}
