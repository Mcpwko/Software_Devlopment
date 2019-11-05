using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class SalariesDB : ISalariesDB
    {
        public IConfiguration Configuration { get; }
        public SalariesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Salaries GetSalary(int IdSalaries)
        {
            Salaries salary = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Salaries WHERE IdSalaries = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", IdSalaries);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            salary = new Salaries();

                            salary.IdSalaries = (int)dr["IdSalaries"];
                            salary.Tips = (float)dr["Tips"];
                            salary.IdCourier = (int)dr["IDCourier"];


                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return salary;

        }





        public List<Salaries> GetSalaries()
        {
            List<Salaries> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Salaries";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Salaries>();

                            Salaries salary = new Salaries();

                            salary.IdSalaries = (int)dr["IdSalaries"];
                            salary.Tips = (float)dr["Tips"];
                            salary.IdCourier = (int)dr["IdCourier"];


                            results.Add(salary);
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


        public Salaries AddSalary(Salaries salary)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into Salaries(Tips,IdCourier) VALUES(@Tips,@IdCourier);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Tips", salary.Tips);
                    cmd.Parameters.AddWithValue("@IdCourier", salary.IdCourier);


                    cn.Open();

                    salary.IdSalaries = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return salary;



        }


        public int UpdateSalary(Salaries salary)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE Salaries SET IdSalaries=@IdSalaries,Tips=@Tips,IdCourier=@IdCourier WHERE IdSalaries=@IdSalaries";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdSalaries", salary.IdSalaries);
                    cmd.Parameters.AddWithValue("@Tips", salary.Tips);
                    cmd.Parameters.AddWithValue("@IdCourier", salary.IdCourier);

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


        public int DeleteSalary(int IdSalaries)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "DELETE FROM Salaries WHERE IdSalaries=@IdSalaries";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdSalaries", IdSalaries);

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
