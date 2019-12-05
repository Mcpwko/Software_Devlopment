using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CityManager : ICityManager
    {
            public ICityDB cityDB { get; }

            public CityManager(IConfiguration configuration)
            {
                cityDB = new CityDB(configuration);
            }



            public List<City> GetCities()
            {
                return cityDB.GetCities();
            }

            public City GetCity(int id)
            {
                return cityDB.GetCity(id);
            }
    }
        
}
