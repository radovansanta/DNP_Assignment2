using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DNP_Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNP_Assignment2.Services
{
    public class UserServices
    {
        public IList<User> Users { get; set; }

        private readonly string usersFile = "users.json";
        
        public UserServices()
        {
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
        
        public void AddUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
        
        public  User ValidateUser(string email, string password)
        {
            User user = Users.FirstOrDefault(takenUser => takenUser.Email.Equals(email));

            if (user==null)
            {
                throw new Exception("User not found");
            }

            if (!user.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return user;
        }
    }
}