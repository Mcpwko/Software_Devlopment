using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DishManager
    {
        public IDishesDB dishDB { get; }

        public DishManager(IConfiguration configuration)
        {
            dishDB = new DishesDB(configuration);
        }



        public List<Dishes> GetDishes(int idRestaurant)
        {
            return dishDB.GetDishes(idRestaurant);
        }

        public Dishes GetDish(int id)
        {
            return dishDB.GetDish(id);
        }
        public Dishes AddDish(Dishes dish)
        {
            return dishDB.AddDish(dish);
        }
        public int UpdateDish(Dishes dish)
        {
            return dishDB.UpdateDish(dish);
        }
        public int DeleteDish(int idDish)
        {
            return dishDB.DeleteDish(idDish);
        }
    }
}
