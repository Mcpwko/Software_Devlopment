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


        //Get all orders
        public List<Order> GetOrders()
        {
            return orderDB.GetOrders();
        }
        //Get one order with IdOrder
        public Order GetOrder(int id)
        {
            return orderDB.GetOrder(id);
        }
        //Add an order
        public Order AddOrder(Order order)
        {
            return orderDB.AddOrder(order);
        }
        //Update an order
        public int UpdateOrder(Order order)
        {
            return orderDB.UpdateOrder(order);
        }
        //Delete an order
        public int DeleteOrder(int idOrder)
        {
            return orderDB.DeleteOrder(idOrder);
        }
        //Get all the orders with the same IdCourier
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
        //Get all the orders with the same IdCustomer
        public List<Order> GetOrdersByCustomer(int id)
        {
            var orderslist = GetOrders();
            var newlist = new List<Order>();

            if (orderslist != null)
            {
                foreach (var order in orderslist)
                {
                    if (order.IdClient == id)
                    {
                        newlist.Add(order);
                    }
                }
            }


            return newlist;
        }

        //Update the status of an order
        public void UpdateOrderStatus(int id)
        {
            Order order = GetOrder(id);
            order.Status = "Completed";
            UpdateOrder(order);

        }
        //Get the number of an order with IdCourier
        public int GetNumberOfOrder(int id, DateTime dateTime)
        {
            return orderDB.GetNumberOfOrder(id, dateTime);
        }



    }
}
