using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CourierDB : ICourierDB
    {
        public IConfiguration Configuration { get; }
        public CourierDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Courier GetCourier(int IdCourier)
        {
            Courier courier = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courier WHERE IdCourier = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", IdCourier);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            courier = new Courier();

                            courier.IdCourier = (int)dr["IdCourier"];
                            courier.IdUser = (int)dr["IdUser"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return courier;

        }





        public List<Courier> GetCouriers()
        {
            List<Courier> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Courier";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Courier>();

                            Courier courier = new Courier();

                            courier.IdCourier = (int)dr["IdCourier"];
                            courier.IdUser = (int)dr["IdUser"];


                            results.Add(courier);
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


        public Courier AddCourier(Courier courier)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Courier(IdCourier,IdUser) VALUES(@IdCourier,@IdUser);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@IdCourier", courier.IdCourier);
                    cmd.Parameters.AddWithValue("@IdUser", courier.IdUser);


                    cn.Open();

                    courier.IdCourier = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return courier;



        }


        public int UpdateCourier(Courier courier)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Courier SET IdCourier=@IdCourier,IdUser=@IdUser WHERE IdCourier=@IdCourier";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdCourier", courier.IdCourier);
                    cmd.Parameters.AddWithValue("@IdUser", courier.IdCourier);

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


        public int DeleteCourier(int IdCourier)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Courier WHERE IdCourier=@IdCourier";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdCourier", IdCourier);

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
