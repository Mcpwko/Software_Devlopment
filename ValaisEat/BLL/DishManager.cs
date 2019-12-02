using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DishManager : IDishManager
    {
        public IDishDB dishDB { get; }

        public DishManager(IConfiguration configuration)
        {
            dishDB = new DishDB(configuration);
        }



        public List<Dish> GetDishes(int idRestaurant)
        {
            return dishDB.GetDishes(idRestaurant);
        }

        public Dish GetDish(int id)
        {
            return dishDB.GetDish(id);
        }
    }
}
