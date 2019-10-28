using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface IRestaurantManager
    {
        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant AddRestaurant(Restaurant restaurant);
        int UpdateRestaurant(Restaurant restaurant);
        int DeleteRestaurant(int idRestaurant);
    }
}
