using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DNP_Assignment2.Models;

namespace DNP_Assignment2.Persistence
{
    public class FileContext
    {
        //public IList<Family> Families { get; private set; }
        public IList<Adult> Adults { get; private set; }
        public IList<User> Users { get; set; }

        private readonly string familiesFile = "families.json";
        private readonly string adultsFile = "adults.json";
        private readonly string usersFile = "users.json";

        public FileContext()
        {
            //Families = File.Exists(familiesFile) ? ReadData<Family>(familiesFile) : new List<Family>();
            Adults = File.Exists(adultsFile) ? ReadData<Adult>(adultsFile) : new List<Adult>();
            Users = File.Exists(usersFile) ? ReadData<User>(usersFile) : new List<User>();
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
            /* storing families
            string jsonFamilies = JsonSerializer.Serialize(Families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(familiesFile, false))
            {
                outputFile.Write(jsonFamilies);
            }
            */

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
            
            // storing users
            string jsonUsers = JsonSerializer.Serialize(Users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            using (StreamWriter outputFile = new StreamWriter(usersFile, false))
            {
                outputFile.Write(jsonUsers);
                Console.Out.WriteLine(jsonUsers);
            }
        }

        public void AddAdult(Adult adult)
        {
            Adults.Add(adult);
            SaveChanges();
        }
        
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }

        public void DeleteAdult(Adult adult)
        {
            Adults.Remove(adult);
            Console.Out.Write(adult);
            SaveChanges();
        }

        public void EditAdult(Adult adult)
        {
            string[] searchOption = {"id", adult.Id.ToString()};
            Adult selectedAdult = SearchAdult(searchOption)[0];
            DeleteAdult(selectedAdult);
            AddAdult(adult);
            SaveChanges();

        }

        public IList<Adult> SearchAdult(string[] value)
        {
            if(value[0] == "firstName")
            {
                return Adults.Where(
                    adult => adult.FirstName.ToLower().Contains(value[1].ToLower())).ToList();
            }
            if(value[0] == "lastName")
            {
                return Adults.Where(
                    adult => adult.LastName.ToLower().Contains(value[1].ToLower())).ToList();
            }
            if(value[0] == "id")
            {
                return Adults.Where(
                    adult => adult.Id.ToString().Contains(value[1])).ToList();
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