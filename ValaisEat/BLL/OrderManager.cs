using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class OrderManager : IOrderManager
    {
        public IOrderDB orderDB { get; }

        public OrderManager(IConfiguration configuration)
        {
            orderDB = new OrderDB(configuration);
        }



        public List<Order> GetOrders()
        {
            return orderDB.GetOrders();
        }

        public Order GetOrder(int id)
        {
            return orderDB.GetOrder(id);
        }
        public Order AddOrder(Order order)
        {
            return orderDB.AddOrder(order);
        }
        public int UpdateOrder(Order order)
        {
            return orderDB.UpdateOrder(order);
        }
        public int DeleteOrder(int idOrder)
        {
            return orderDB.DeleteOrder(idOrder);
        }

        public List<Order> GetOrdersByCourier(int id)
        {
            var orderslist = GetOrders();
            var newlist = new List<Order>();

            if (orderslist != null) { 
            foreach(var order in orderslist)
            {
                if (order.IdCourier == id)
                {
                    newlist.Add(order);
                }
            }
            }


            return newlist;
        }


        public void UpdateOrderStatus(int id)
        {
            Order order = GetOrder(id);
            order.Status = "Completed";
            UpdateOrder(order);

        }
    }
}
