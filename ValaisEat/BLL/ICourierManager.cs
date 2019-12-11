using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ICourierManager
    {
        List<Courier> GetCouriers();
        Courier GetCourier(int IdCourier);
        Courier GetCourierByUserId(int id);
        List<Courier> GetCouriersByUserIdSameCity(List<User> users);
    }
}
