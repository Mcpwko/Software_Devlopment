using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CourierManager
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

        public int UpdateCourier(Courier courier)
        {
            return courierDB.UpdateCourier(courier);
        }
        
        public int DeleteCourier(Courier courier)
        {
            return courierDB.DeleteCourier(courier);
        }

    }
}
