using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrderManager
    {
        List<Order> GetOrders();
        Order GetOrder(int id);
        Order AddOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int idOrder);
        List<Order> GetOrdersByCourier(int id);
        List<Order> GetOrdersByCustomer(int id);
        void UpdateOrderStatus(int id);
        int GetNumberOfOrder(int id);
    }
}
