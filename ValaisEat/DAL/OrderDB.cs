using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderDB : IOrderDB
    {
        public IConfiguration Configuration { get; }
        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Order GetOrder(int id)
        {
            Order order = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Order] WHERE IdOrder = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            order = new Order();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.Status = (string)dr["Status"];
                            order.Date = (DateTime)dr["Date"];
                            order.ShippingDate = (DateTime)dr["ShippingDate"];
                            order.TotalPrice = (double)dr["TotalPrice"];
                            order.IdCourier = (int)dr["IdCourier"];
                            order.IdClient = (int)dr["IdClient"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;

        }





        public List<Order> GetOrders()
        {
            List<Order> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Order]";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order>();

                            Order order = new Order();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.Status = (string)dr["Status"];
                            order.Date = (DateTime)dr["Date"];
                            order.ShippingDate = (DateTime)dr["ShippingDate"];
                            order.TotalPrice = (double)dr["TotalPrice"];
                            order.IdCourier = (int)dr["IdCourier"];
                            order.IdClient = (int)dr["IdClient"];



                            results.Add(order);
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


        public Order AddOrder(Order order)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into [Order](Status,Date,ShippingDate,TotalPrice,IdCourier,IdClient) VALUES(@Status,@Date,@ShippingDate,@TotalPrice,@IdCourier,@IdClient);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@Status", order.Status);
                    cmd.Parameters.AddWithValue("@Date", order.Date);
                    cmd.Parameters.AddWithValue("@ShippingDate", order.ShippingDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@IdCourier", order.IdCourier);
                    cmd.Parameters.AddWithValue("@IdClient", order.IdClient);


                    cn.Open();

                    order.IdOrder = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;



        }


        public int UpdateOrder(Order order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE [Order] SET Status=@Status,Date=@Date,ShippingDate=@ShippingDate,TotalPrice=@TotalPrice,IdCourier=@IdCourier,IdClient=@IdClient WHERE IdOrder=@IdOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                    cmd.Parameters.AddWithValue("@Status", order.Status);
                    cmd.Parameters.AddWithValue("@Date", order.Date);
                    cmd.Parameters.AddWithValue("@ShippingDate", order.ShippingDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@IdCourier", order.IdCourier);
                    cmd.Parameters.AddWithValue("@IdClient", order.IdClient);

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


        public int DeleteOrder(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM [Order] WHERE IdOrder=@IdOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdOrder", idOrder);

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

        public int GetNumberOfOrder(int id)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result =0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "SELECT COUNT(IdOrder)FROM [Order]WHERE ShippingDate <=dateadd(minute, +30, GetDate()) AND ShippingDate>=dateadd(minute,0,GETDATE()) AND IdCourier=@IdCourier AND Status='Not delivered' GROUP BY IdCourier ";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdCourier", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            result = (int)dr[0];





                        }
                    }

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
