using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CourierManager : ICourierManager
    {
        public ICourierDB courierDB { get; }

        public CourierManager(IConfiguration configuration)
        {
            courierDB = new CourierDB(configuration);
        }

        public List<Courier> GetCouriers()
        {
            return courierDB.GetCouriers();
        }

        public Courier GetCourier(int IdCourier)
        {
            return courierDB.GetCourier(IdCourier);
        }

        public Courier GetCourierByUserId(int id)
        {
            var users = GetCouriers();
            var user = new Courier();

            foreach(var user1 in users)
            {
                if (user1.IdUser == id)
                    user = user1;
            }

            return user;
        }


    }
}
