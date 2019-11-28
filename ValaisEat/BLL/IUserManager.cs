using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IUserManager
    {
        List<User> GetUsers();
        User GetUser(int idUser);
        User AddUser(User user);
        int UpdateUser(User user);
        int DeleteUser(int idUser);
    }
}
