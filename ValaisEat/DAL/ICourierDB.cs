using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface ICourierDB
    {
        IConfiguration Configuration { get; }

        List<Courier> GetCouriers();
        Courier GetCourier(int IdCourier);
        int UpdateCourier(Courier courier);


    }
}
