using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DishesDB : IDishesDB
    {
        public IConfiguration Configuration { get; }
        public DishesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Dishes GetDishes(int id)
        {
            Dishes dish = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes WHERE IdDishes = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            dish = new Dishes();

                            dish.IdDishes = (int)dr["IdDishes"];
                            dish.Name = (string)dr["Name"];
                            dish.Description = (string)dr["Description"];
                            dish.Price = (float)dr["Price"];
                            dish.Title = (string)dr["Title"];
                            dish.Status = (string)dr["Status"];
                            dish.IdRestaurants = (int)dr["IdRestaurants"];


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





        public List<Dishes> GetDishes()
        {
            List<Dishes> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dishes";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dishes>();

                            Dishes dish = new Dishes();

                            dish.IdDishes = (int)dr["IdDishes"];
                            dish.Name = (string)dr["Name"];
                            dish.Description = (string)dr["Description"];
                            dish.Price = (float)dr["Price"];
                            dish.Title = (string)dr["Title"];
                            dish.Status = (string)dr["Status"];
                            dish.IdRestaurants = (int)dr["IDRestaurants"];


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


        public Dishes AddDish(Dishes dish)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Dishes(Name,Description,Price,Title,Status,IdRestaurants) VALUES(@Name,@Description,@Price,@Title,@Status,@IdRestaurants);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Description", dish.Description);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@Title", dish.Title);
                    cmd.Parameters.AddWithValue("@Status", dish.Status);
                    cmd.Parameters.AddWithValue("@IdRestaurants", dish.IdRestaurants);


                    cn.Open();

                    dish.IdDishes = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dish;



        }


        public int UpdateDish(Dishes dish)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Dishes SET Name=@Name,Description=@Description,Price=@Price,Title=@Title,Status=@Status,IdRestaurants=@IdRestaurants WHERE IdDishes=@IdDishes";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdRestaurants", dish.IdRestaurants);
                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Description", dish.Description);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@Title", dish.Title);
                    cmd.Parameters.AddWithValue("@Status", dish.Status);
                    cmd.Parameters.AddWithValue("@IdRestaurants", dish.IdRestaurants);


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


        public int DeleteDish(int idDish)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Dishes WHERE IdDishes=@IdDishes";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdDishes", idDish);

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
