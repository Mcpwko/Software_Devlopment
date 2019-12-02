using DTO;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDishDB
    {

        List<Dish> GetDishes(int idRestaurant);
        Dish GetDish(int id);
    }
}
