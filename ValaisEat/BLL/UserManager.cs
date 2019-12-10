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
        public List<User> GetUsers()
        {
            return userDB.GetUsers();
        }
        public User GetUser(int id)
        {
            return userDB.GetUser(id);
        }
        public User AddUser(User user)
        {
            return userDB.AddUser(user);
        }
        public int UpdateUser(User user)
        {
            return userDB.UpdateUser(user);
        }

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


        

    }
}
