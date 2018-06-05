using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DierenAsielASP.Models;

namespace DierenAsielASP.Database
{
    public class DatabaseManager : IDatabase
    {
        public static string ConnectionString;

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

        private IEnumerable<IDataRecord> CreateReader(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader;
                        }
                    }
                }
            }
        }

        public List<AnimalModel> GetAllAnimals()
        {
            List<AnimalModel> AllAnimals = new List<AnimalModel>();
            string query = $"Select * from Animals";
            SqlParameter[] parameters = { };

            IEnumerable<IDataRecord> reader = CreateReader(query, parameters);
            foreach (IDataRecord record in reader)
            {
                AnimalModel temp = new AnimalModel()
                {
                    name = record["Name"].ToString(),
                    age = (int)record["Age"],
                    weight = (int)record["Weight"],
                    gender = (AnimalModel.Genders)Enum.Parse(typeof(AnimalModel.Genders), record["Gender"].ToString()),
                    species = (AnimalModel.Species)Enum.Parse(typeof(AnimalModel.Species), record["Species"].ToString()),
                    image = record["Image"].ToString(),
                    cage = (int)record["Cage"],
                    price = (float)(double)record["Price"], //<- Blame microsoft
                    reserved = (bool)record["Reserved"],
                    breed = record["Breed"].ToString(),
                    about = record["About"].ToString()
                };
                temp.characteristics = GetCharacteristicsFromAnimal(temp);
                AllAnimals.Add(temp);
            }
            return AllAnimals;
        }

        public List<string> GetCharacteristicsFromAnimal(AnimalModel animal)
        {
            List<string> characteristics = new List<string>();
            string query = $"select Characteristic from Characteristics where AnimalId = (select id from Animals where Name = @Name and Age = @Age and Weight = @Weight and Gender = @Gender and Species = @Species and Cage = @Cage)";
            SqlParameter[] parameters =
            {
                new SqlParameter("Name", animal.name),
                new SqlParameter("Age", animal.age),
                new SqlParameter("Weight", animal.weight),
                new SqlParameter("Gender", animal.gender.ToString()),
                new SqlParameter("Species", animal.species.ToString()),
                new SqlParameter("Cage", animal.cage)
            };
            foreach (IDataRecord record in CreateReader(query, parameters))
            {
                characteristics.Add(record["Characteristic"].ToString());
            }

            return characteristics;
        }

        public List<AnimalModel> GetAllAnimalsNotReserved()
        {
            return GetAllAnimals().Where(x => !x.reserved).ToList();
        }
    }
}
