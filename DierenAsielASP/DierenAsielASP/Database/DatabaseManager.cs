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
    public static class DatabaseManager
    {
        public static string ConnectionString;

        private static void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static IEnumerable<IDataRecord> CreateReader(string query)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static List<AnimalModel> GetAllAnimals()
        {
            List<AnimalModel> AllAnimals = new List<AnimalModel>();
            string query = $"Select * from Dieren";

            IEnumerable<IDataRecord> reader = CreateReader(query);
            foreach (IDataRecord record in reader)
            {
                AnimalModel temp = new AnimalModel()
                {
                    name = record["Naam"].ToString(),
                    age = (int)record["Leeftijd"],
                    weight = (int)record["Gewicht"],
                    gender = (AnimalModel.Genders)Enum.Parse(typeof(AnimalModel.Genders), record["Geslacht"].ToString()),
                    species = (AnimalModel.Species)Enum.Parse(typeof(AnimalModel.Species), record["Soort"].ToString()),
                    image = record["Afbeelding"].ToString(),
                    cage = (int)record["HokNummer"],
                    price = float.Parse(record["Prijs"].ToString()),
                    reserved = Convert.ToBoolean(int.Parse(record["Gereserveerd"].ToString()))
                };
                temp.characteristics = GetCharacteristicsFromAnimal(temp);
                AllAnimals.Add(temp);
            }
            return AllAnimals;
        }

        public static List<string> GetCharacteristicsFromAnimal(AnimalModel animal)
        {
            List<string> characteristics = new List<string>();
            string query = $"select Eigenschap from Eigenschappen where DierId = (select id from Dieren where Naam = '{animal.name}' and Leeftijd = {animal.age} and Gewicht = {animal.weight} and Geslacht = '{animal.gender.ToString()}' and Soort = '{animal.species.ToString()}' and HokNummer = {animal.cage})";

            foreach (IDataRecord record in CreateReader(query))
            {
                characteristics.Add(record["Eigenschap"].ToString());
            }

            return characteristics;
        }
    }
}
