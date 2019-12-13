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
        //Get all the couriers
        public List<Courier> GetCouriers()
        {
            return courierDB.GetCouriers();
        }
        //Get a courier with IdCourier
        public Courier GetCourier(int IdCourier)
        {
            return courierDB.GetCourier(IdCourier);
        }
        //Get a courier with IdUser
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
        //Get the list of courier that are in the same city
        public List<Courier> GetCouriersByUserIdSameCity(List<User> users)
        {

            var couriers = new List<Courier>();

            foreach(var user in users)
            {
                if (GetCourierByUserId(user.IdUser).IdCourier!=0)
                {
                    var courier = GetCourierByUserId(user.IdUser);
                    couriers.Add(courier);
                }
                    
            }

            return couriers;
        }



    }
}
