using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IUserDB
    {
        List<User> GetUsers();
        User GetUser(int id);
        User AddUser(User user);
        int UpdateUser(User user);
    }
}
