using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomerDB
    {
        IConfiguration Configuration { get; }

        List<Customer> GetCustomer();
        Customer GetCustomer(int id);
        Customer AddCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(int idCustomer);

        
    }
}
