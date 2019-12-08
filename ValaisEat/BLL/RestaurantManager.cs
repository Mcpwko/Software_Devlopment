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



        public List<Restaurant> GetRestaurants()
        {
            return restaurantDB.GetRestaurants();
        }

        public Restaurant GetRestaurant(int id)
        {
            return restaurantDB.GetRestaurant(id);
        }

    }
}
