using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class RestaurantDB : IRestaurantDB
    {
        private string connectionString = null;
        public RestaurantDB(IConfiguration configuration)
        {
            var config = configuration;
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public Restaurant GetRestaurant(int id)
        {
            Restaurant restaurant = null;
            


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurant WHERE IdRestaurant = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["IdRestaurant"];
                            restaurant.Name = (string)dr["Name"];
                            restaurant.Openingdate = (DateTime)dr["OpeningDate"];
                            restaurant.Schedule = (string)dr["Schedule"];
                            restaurant.Type = (string)dr["Type"];
                            restaurant.Adress = (string)dr["Adress"];
                            restaurant.IdCity = (int)dr["IdCity"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurant;

        }





        public List<Restaurant> GetRestaurants()
        {
            List<Restaurant> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurant>();

                            Restaurant restaurant = new Restaurant();

                            restaurant.IdRestaurant = (int)dr["IdRestaurant"];
                            restaurant.Name = (string)dr["Name"];
                            restaurant.Openingdate = (DateTime)dr["OpeningDate"];
                            restaurant.Schedule = (string)dr["Schedule"];
                            restaurant.Type = (string)dr["Type"];
                            restaurant.Adress = (string)dr["Adress"];
                            restaurant.IdCity = (int)dr["IdCity"];


                            results.Add(restaurant);
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
