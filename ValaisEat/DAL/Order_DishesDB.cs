using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class Order_DishesDB : IOrder_DishesDB
    {
        public IConfiguration Configuration { get; }
        public Order_DishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Order_Dishes GetOrder_Dishes(int id)
        {
            Order_Dishes order = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Order_Dishes WHERE IdOrder = @IdOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            order = new Order_Dishes();

                            order.IdOrder = (int)dr["IdOrder"];
                            order.IdDishes = (int)dr["IdDishes"];
                            order.Quantity = (int)dr["Quantity"];

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




        public Order_Dishes AddOrder_Dishes(Order_Dishes order)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Order_Dishes(Quantity,IdDishes,IdOrder) VALUES(@Quantity,@IdDishes,@IdOrder)";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@Status", order.Quantity);
                    cmd.Parameters.AddWithValue("@Date", order.IdDishes);
                    cmd.Parameters.AddWithValue("@ShippingDate", order.IdOrder);


                    cn.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;



        }


        public int UpdateOrder_Dishes(Order_Dishes order)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Order_Dishes SET IdOrder=@IdOrder,IdDishes=@IdDishes,Quantity=@Quantity WHERE IdOrder=@IdOrder";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                    cmd.Parameters.AddWithValue("@Status", order.IdDishes);
                    cmd.Parameters.AddWithValue("@Date", order.Quantity);

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


        public int DeleteOrder_Dishes(int idOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Order_Dishes WHERE IdOrder=@IdOrder";
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
    }
}
