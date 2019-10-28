using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class RestaurantManager
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
        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            return restaurantDB.AddRestaurant(restaurant);
        }
        public int UpdateHotel(Restaurant restaurant)
        {
            return restaurantDB.UpdateRestaurant(restaurant);
        }
        public int DeleteRestaurant(int idRestaurant)
        {
            return restaurantDB.DeleteRestaurant(idRestaurant);
        }
    }
}
