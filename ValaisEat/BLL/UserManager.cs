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

        public bool VerificateAuthentification(string username, string password)
        {

            string motDePasse = password;
            return DTO.User.FirstOrDefault(u => u.Prenom == nom && u.MotDePasse == motDePasseEncode);

        }

    }
}
