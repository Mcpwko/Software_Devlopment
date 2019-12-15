using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager
    {
        public IRestaurantDB restaurantDB { get; }

        public RestaurantManager(IConfiguration configuration)
        {
            restaurantDB = new RestaurantDB(configuration);
        }


        //Get all the restaurants
        public List<Restaurant> GetRestaurants()
        {
            return restaurantDB.GetRestaurants();
        }
        //Get a restaurant with an IdRestaurant
        public Restaurant GetRestaurant(int id)
        {
            return restaurantDB.GetRestaurant(id);
        }
        //Get the city of a Restaurant
        public int GetCityFromRestaurant(int id)
        {
            Restaurant resto = GetRestaurant(id);
            int idcity = resto.IdCity;
            return idcity;
        }

    }
}
