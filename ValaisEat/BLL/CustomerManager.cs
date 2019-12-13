using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class CustomerManager : ICustomerManager
    {
        public ICustomerDB customerDB { get; }

        public CustomerManager(IConfiguration configuration)
        {
            customerDB = new CustomerDB(configuration);
        }


        //Get all the customers
        public List<Customer> GetCustomers()
        {
            return customerDB.GetCustomers();
        }
        //Get a customer with idClient
        public Customer GetCustomer(int idClient)
        {
            return customerDB.GetCustomer(idClient);
        }
        //Add a customer into the database
        public Customer AddCustomer(Customer customer)
        {
            return customerDB.AddCustomer(customer);
        }

        //Check if the user is a customer
        public bool IsACustomer(User user)
        {

            var list = GetCustomers();


            foreach (var user2 in list)
            {
                if (user2.IdUser == user.IdUser)
                {
                    return true;
                }
            }
            return false;
        }

        //Get the customer with IdUser
        public int GetCustomerByIdUser(int id)
        {
            var list = GetCustomers();

            foreach(var customer in list)
            {
                if (customer.IdUser == id)
                {
                    return customer.IdClient;
                }
                
            }
            return 0;
        }
    }
}
