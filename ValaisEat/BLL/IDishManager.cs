using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IDishManager
    {
        List<Dishes> GetDishes(int idRestaurant);
        Dishes GetDish(int id);
        Dishes AddDish(Dishes dish);
        int UpdateDish(Dishes dish);
        int DeleteDish(int idDish);
    }
}
