using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DishDB : IDishDB
    {
        private string connectionString = null;
        public DishDB(IConfiguration configuration)
        {
            var config = configuration;
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public Dish GetDish(int id)
        {
            Dish dish = null;


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish WHERE IdDish = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            dish = new Dish();

                            dish.IdDish = (int)dr["IdDish"];
                            dish.Name = (string)dr["Name"];
                            dish.Description = (string)dr["Description"];
                            dish.Price = (double)dr["Price"];
                            dish.Title = (string)dr["Title"];
                            dish.Status = (string)dr["Status"];
                            dish.IdRestaurant = (int)dr["IdRestaurant"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dish;

        }





        public List<Dish> GetDishes(int idRestaurant)
        {
            List<Dish> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish WHERE IdRestaurant =@idRestaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idRestaurant", idRestaurant);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dish>();

                            Dish dish = new Dish();

                            dish.IdDish = (int)dr["IdDish"];
                            dish.Name = (string)dr["Name"];
                            dish.Description = (string)dr["Description"];
                            dish.Price = (double)dr["Price"];
                            dish.Title = (string)dr["Title"];
                            dish.Status = (string)dr["Status"];
                            dish.IdRestaurant = (int)dr["IDRestaurant"];


                            results.Add(dish);
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

    }
}
