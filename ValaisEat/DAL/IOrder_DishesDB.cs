using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrder_DishesDB
    {
        IConfiguration Configuration { get; }

        Order_Dishes GetOrder_Dishes(int id);
        Order_Dishes AddOrder_Dishes(Order_Dishes order_dishes);
        int UpdateOrder_Dishes(Order_Dishes order_dishes);
        int DeleteOrder_Dishes(int idOrder);
    }
}
