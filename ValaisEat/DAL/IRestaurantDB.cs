using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IRestaurantDB
    {
        IConfiguration Configuration { get; }

        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant AddRestaurant(Restaurant restaurant);
        int UpdateRestaurant(Restaurant restaurant);
        int DeleteRestaurant(int idRestaurant);
    }
}
