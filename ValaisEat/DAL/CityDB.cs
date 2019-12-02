using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CityDB : ICityDB
    {
        public IConfiguration Configuration { get; }
        public CityDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public City GetCity(int id)
        {
            City city = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM City WHERE IdCity = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            city = new City();

                            city.IdCity = (int)dr["IdCity"];
                            city.NPA = (int)dr["NPA"];
                            city.CityName = (string)dr["CityName"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return city;

        }





        public List<City> GetCities()
        {
            List<City> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM City";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<City>();

                            City city = new City();

                            city.IdCity = (int)dr["IdCity"];
                            city.NPA = (int)dr["NPA"];
                            city.CityName = (string)dr["CityName"];


                            results.Add(city);
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
