using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CitiesManager
    {
            public ICitiesDB cityDB { get; }

            public CitiesManager(IConfiguration configuration)
            {
                cityDB = new CitiesDB(configuration);
            }



            public List<Cities> GetCities()
            {
                return cityDB.GetCities();
            }

            public Cities GetCity(int id)
            {
                return cityDB.GetCity(id);
            }
            public Cities AddCity(Cities city)
            {
                return cityDB.AddCity(city);
            }
            public int UpdateCity(Cities city)
            {
                return cityDB.UpdateCity(city);
            }
            public int DeleteCity(int idCity)
            {
                return cityDB.DeleteCity(idCity);
            }
    }
        
}
