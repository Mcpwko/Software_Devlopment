using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CustomerDB : ICustomerDB
    {
        private string connectionString = null;
        public CustomerDB(IConfiguration configuration)
        {
            var config = configuration;
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public Customer GetCustomer(int id)
        {
            Customer customer = null;


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE IdClient = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            customer = new Customer();

                            customer.IdClient = (int)dr["IdClient"];
                            customer.IdUser = (int)dr["IdUser"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;

        }





        public List<Customer> GetCustomers()
        {
            List<Customer> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Customer>();

                            Customer customer = new Customer();

                            customer.IdClient = (int)dr["IdClient"];
                            customer.IdUser = (int)dr["IdUser"];


                            results.Add(customer);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }


        public Customer AddCustomer(Customer customer)
        {


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Customer(IdUser) VALUES(@IdUser); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdUser", customer.IdUser);


                    cn.Open();

                    customer.IdClient = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;



        }


        public int UpdateCustomer(Customer customer)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Customer SET IdClient=@IdClient,IdUser=@IdUser WHERE IdClient=@IdClient";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdClient", customer.IdClient);
                    cmd.Parameters.AddWithValue("@IdUser", customer.IdUser);
          
                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }


        public int DeleteCustomer(int IdClient)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Customer WHERE IdClient=@IdClient";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdClient", IdClient);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }

    }
}
