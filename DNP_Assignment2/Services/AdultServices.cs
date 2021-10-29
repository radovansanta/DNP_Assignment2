using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment2.Models;

namespace DNP_Assignment2.Services
{
    public class AdultServices
    {
        public IList<Adult> Adults { get; private set; }
        
        private readonly string adultsFile = "adults.json";
        
        public AdultServices()
        {
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
        }
        
        private IList<T> ReadData<T>(string s)
        {
            using (var jsonReader = File.OpenText(s))
            {
                return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd());
            }
        }
        
        public void SaveChanges()
        {
            // storing persons
            string jsonAdults = JsonSerializer.Serialize(Adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(adultsFile, false))
            {
                outputFile.Write(jsonAdults);
                Console.Out.WriteLine(jsonAdults);
            }
        }
        
        public void AddAdult(Adult adult)
        {
            if (IsIdUnique(adult.Id))
            {
                Adults.Add(adult);
                SaveChanges();
            }
        }
        
        public async Task DeleteAdult(Adult adult)
        {
            Adults.Remove(adult);
            Console.Out.Write(adult);
            SaveChanges();
        }
        
        public async Task UpdateAdult(int id, Adult updatedAdult)
        {
            Adult adult = SearchAdult("id",id.ToString())[0];
            int index = Adults.IndexOf(adult);
            Adults.RemoveAt(index);
            Adults.Insert(index,updatedAdult);
            SaveChanges();
        }

        public IList<Adult> SearchAdult(string type, string value)
        {
            if(type == "firstName")
            {
                return Adults.Where(
                    adult => adult.FirstName.ToLower().Contains(value.ToLower())).ToList();
            }
            if(type == "lastName")
            {
                return Adults.Where(
                    adult => adult.LastName.ToLower().Contains(value.ToLower())).ToList();
            }
            if(type == "id")
            {
                return Adults.Where(
                    adult => adult.Id.ToString().Contains(value)).ToList();
            }
            return Adults;
        }

        public Boolean IsIdUnique(int id)
        {
            for (int i = 0; i < Adults.Count; i++)
            {
                if (Adults[i].Id == id)
                {
                    return false;
                }
                
            }
            return true;
        }
    }
}