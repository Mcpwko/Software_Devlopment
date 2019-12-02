﻿using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICitiesManager
    {
        List<City> GetCities();
        City GetCity(int id);
        City AddCity(City city);
        int UpdateCity(City city);
        int DeleteCity(int idCity);
    }
}
