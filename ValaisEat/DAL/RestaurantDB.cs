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
        public IConfiguration Configuration { get; }
        public RestaurantDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Restaurant GetRestaurant(int id)
        {
            Restaurant restaurant = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants WHERE IdRestaurants = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            restaurant = new Restaurant();

                            restaurant.IdRestaurants = (int)dr["IdRestaurants"];
                            restaurant.Name = (string)dr["Name"];
                            restaurant.Openingdate = (string)dr["OpeningDate"];
                            restaurant.Schedule = (string)dr["Schedule"];
                            restaurant.Type = (string)dr["Type"];
                            restaurant.Adress = (string)dr["Adress"];
                            restaurant.IdCities = (int)dr["IdCities"];


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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurant>();

                            Restaurant restaurant = new Restaurant();

                            restaurant.IdRestaurants = (int)dr["IdRestaurants"];
                            restaurant.Name = (string)dr["Name"];
                            restaurant.Openingdate = (string)dr["OpeningDate"];
                            restaurant.Schedule = (string)dr["Schedule"];
                            restaurant.Type = (string)dr["Type"];
                            restaurant.Adress = (string)dr["Adress"];
                            restaurant.IdCities = (int)dr["IdCities"];


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


        public Restaurant AddRestaurant(Restaurant restaurant)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Restaurants(Name,Openingdate,Schedule,Type,Adress,IdCities) VALUES(@Name,@Openingdate,@Schedule,@Type,@Adress,@IdCities);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Description", restaurant.Openingdate);
                    cmd.Parameters.AddWithValue("@Location", restaurant.Schedule);
                    cmd.Parameters.AddWithValue("@Category", restaurant.Type);
                    cmd.Parameters.AddWithValue("@HasWifi", restaurant.Adress);
                    cmd.Parameters.AddWithValue("@HasParking", restaurant.IdCities);
  

                    cn.Open();

                    restaurant.IdRestaurants = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurant;



        }


        public int UpdateRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Restaurants SET Name=@Name,Openingdate=@Openingdate,Schedule=@Schedule,Type=@Type,Adress=@Adress,IdCities=@IdCities WHERE IdRestaurants=@IdRestaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdRestaurants", restaurant.IdRestaurants);
                    cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@Description", restaurant.Openingdate);
                    cmd.Parameters.AddWithValue("@Location", restaurant.Schedule);
                    cmd.Parameters.AddWithValue("@Category", restaurant.Type);
                    cmd.Parameters.AddWithValue("@HasWifi", restaurant.Adress);
                    cmd.Parameters.AddWithValue("@HasParking", restaurant.IdCities);
           

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


        public int DeleteRestaurant(int idRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Restaurants WHERE IdRestaurants=@IdRestaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdRestaurants", idRestaurant);

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
