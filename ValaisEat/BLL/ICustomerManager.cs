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
        int UpdateCustomer(Customer customer);
        int DeleteCustomer(int idClient);

        bool IsACustomer(User user);

        int GetCustomerByIdUser(int id);
    }
}
