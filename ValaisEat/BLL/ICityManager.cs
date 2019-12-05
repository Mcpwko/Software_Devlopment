using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICityManager
    {
        List<City> GetCities();
        City GetCity(int id);
    }
}
