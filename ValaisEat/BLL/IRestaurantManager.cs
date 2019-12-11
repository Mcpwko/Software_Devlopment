using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IRestaurantManager
    {
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        int GetCityFromRestaurant(int id);
    }
}
