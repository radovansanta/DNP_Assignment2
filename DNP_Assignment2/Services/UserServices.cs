using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DNP_Assignment2.Models;
using DNP_Assignment2.Persistence;

namespace DNP_Assignment2.Services
{
    public class UserServices
    {
        private List<User> users;
        
        public User ValidateUser(string email, string password)
        {
            FileContext fileContext = new();
            users = fileContext.Users.ToList();
            User user = users.FirstOrDefault(takenUser => takenUser.Email.Equals(email));

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