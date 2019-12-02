using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IUserManager
    {
        User GetUser(int idUser);
        User AddUser(User user);
        int UpdateUser(User user);
    }
}
