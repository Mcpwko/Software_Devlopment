using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Order_DishesManager : IOrder_DishesManager
    {
        public IOrder_DishesDB orderDishesDB { get; }

        public Order_DishesManager(IConfiguration configuration)
        {
            orderDishesDB = new Order_DishesDB(configuration);
        }


        public List<Order_Dishes> GetOrder_Dishes(int id)
        {
            return orderDishesDB.GetOrder_Dishes(id);
        }
        public Order_Dishes AddOrder_Dishes(Order_Dishes order)
        {
            return orderDishesDB.AddOrder_Dishes(order);
        }
        public int UpdateOrder_Dishes(Order_Dishes order)
        {
            return orderDishesDB.UpdateOrder_Dishes(order);
        }
        public int DeleteOrder_Dishes(int idOrder)
        {
            return orderDishesDB.DeleteOrder_Dishes(idOrder);
        }
    }
}
