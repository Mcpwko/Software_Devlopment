using DTO;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDishesDB
    {
        IConfiguration Configuration { get; }

        List<Dishes> GetDishes(int idRestaurant);
        Dishes GetDish(int id);
        Dishes AddDish(Dishes dish);
        int UpdateDish(Dishes dish);
        int DeleteDish(int idDish);
    }
}
