using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserManager : IUserManager
    {
        public IUserDB userDB { get; }

        public UserManager(IConfiguration configuration)
        {
            userDB = new UserDB(configuration);
        }
        //Get all the Users
        public List<User> GetUsers()
        {
            return userDB.GetUsers();
        }
        //Get a user with IdUser
        public User GetUser(int id)
        {
            return userDB.GetUser(id);
        }
        //Add a user
        public User AddUser(User user)
        {
            return userDB.AddUser(user);
        }
        //INUTILE
        public int UpdateUser(User user)
        {
            return userDB.UpdateUser(user);
        }
        //Check the authetication of a user
        public bool VerificateAuthentification(string username, string password)
        {
            
            var users = GetUsers();

            foreach (var user in users)
            {
                if (user.Email.Equals(username) && user.Password.Equals(password))
                {
                    return true;
                }
            }

            return false;
        }

        //Get a user with Email
        public User GetUserByEmail(string email)
        {
            var users = GetUsers();

            foreach(User user in users)
            {
                if (user.Email.Equals(email))
                    return user;
            }

            return null;

            
        }
        //Check if a user already exists
        public bool UserAlreadyExist(string email)
        {
            var users = GetUsers();

            foreach (User user in users)
            {
                if (user.Email.Equals(email))
                {
                    return true;
                }

            }

            return false;
        }

        //Get city of a user
        public int GetCityFromUser(int id)
        {
            User user = GetUser(id);
            int city = user.IdCity;

            return city;

        }
        //Get all the users from a city
        public List<User> GetUsersByIdCity(int id)
        {
            var users = GetUsers();
            var userInTheSameCity = new List<User>();

            foreach(var user in users)
            {
                if (user.IdCity == id)
                    userInTheSameCity.Add(user);
            }

            return userInTheSameCity;
        }

    }
}
