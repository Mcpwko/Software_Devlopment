using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IOrderDB
    {
        IConfiguration Configuration { get; }

        List<Order> GetOrders();
        Order GetOrder(int id);
        Order AddOrder(Order order);
        int UpdateOrder(Order order);
        int DeleteOrder(int idOrder);
        int GetNumberOfOrder(int id);
    }
}
