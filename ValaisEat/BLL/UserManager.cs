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
        public int DeleteUser(int idUser)
        {
            return userDB.DeleteUser(idUser);
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

    }
}
