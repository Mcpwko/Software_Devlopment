using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CustomerManager
    {
        public ICustomerDB customerDB { get; }

        public CustomerManager(IConfiguration configuration)
        {
            customerDB = new CustomerDB(configuration);
        }



        public List<Customer> GetCustomers()
        {
            return customerDB.GetCustomers();
        }

        public Customer GetCustomer(int idClient)
        {
            return customerDB.GetCustomer(idClient);
        }
        public Customer AddCustomer(Customer customer)
        {
            return customerDB.AddCustomer(customer);
        }
        public int UpdateCustomer(Customer customer)
        {
            return customerDB.UpdateCustomer(customer);
        }
        public int DeleteCustomer(int idClient)
        {
            return customerDB.DeleteCustomer(idClient);
        }
    }
}
