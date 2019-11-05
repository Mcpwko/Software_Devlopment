using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICitiesManager
    {
        List<Cities> GetCities();
        Cities GetCity(int id);
        Cities AddCity(Cities city);
        int UpdateCity(Cities city);
        int DeleteCity(int idCity);
    }
}
