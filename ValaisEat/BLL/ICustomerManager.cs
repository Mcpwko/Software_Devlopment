using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public interface ICustomerManager
    {
        List<Customer> GetCustomers();
        Customer GetCustomer(int idClient);
        Customer AddCustomer(Customer customer);

        bool IsACustomer(User user);

        int GetCustomerByIdUser(int id);
    }
}
