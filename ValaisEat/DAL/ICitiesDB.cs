using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ICitiesDB
    {
        IConfiguration Configuration { get; }

        List<Cities> GetCities();
        Cities GetCity(int id);
        Cities AddCity(Cities city);
        int UpdateCity(Cities city);
        int DeleteCity(int idCity); 
    }
}
