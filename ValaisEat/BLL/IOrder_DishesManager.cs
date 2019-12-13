using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrder_DishesManager
    {
        List<Order_Dishes> GetOrder_Dishes(int id);
        Order_Dishes AddOrder_Dishes(Order_Dishes order);
        int DeleteOrder_Dishes(int idOrder);
    }
}
