using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IRestaurantDB
    {

        List<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
    }
}
