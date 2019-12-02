using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class UserDB : IUserDB
    {
        private string connectionString = null;
        public UserDB(IConfiguration configuration)
        {
            var config = configuration;
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public User GetUser(int idUser)
        {
            User user = null;


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [User] WHERE IdUser = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", idUser);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            user = new User();

                            user.IdUser = (int)dr["IdUser"];
                            user.Name = (string)dr["Name"];
                            user.Firstname = (string)dr["Firstname"];
                            user.Adress = (string)dr["Adress"];
                            user.Telephon = (string)dr["Telephon"];
                            user.Email = (string)dr["Email"];
                            user.Password = (string)dr["Password"];
                            user.Date = (DateTime)dr["Date"];
                            user.IdCity = (int)dr["IdCity"];



                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return user;

        }





        public List<User> GetUsers()
        {
            List<User> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [User]";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<User>();

                            User user = new User();

                            user.IdUser = (int)dr["IdUser"];
                            user.Name = (string)dr["Name"];
                            user.Firstname = (string)dr["Firstname"];
                            user.Adress = (string)dr["Adress"];
                            user.Telephon = (string)dr["Telephon"];
                            user.Email = (string)dr["Email"];
                            user.Password = (string)dr["Password"];
                            user.Date = (DateTime)dr["Date"];
                            user.IdCity = (int)dr["IdCity"];


                            results.Add(user);
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


        public User AddUser(User user)
        {


            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT into [User](Name,Firstname,Adress,Telephon,Email,Password,Date,IdCities) VALUES(@Name,@Firstname,@Adress,@Telephon,@Email,@Password,@Date,@IdCity);SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);



                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Adress", user.Adress);
                    cmd.Parameters.AddWithValue("@Telephon", user.Telephon);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Date", user.Date);
                    cmd.Parameters.AddWithValue("@IdCity", user.IdCity);


                    cn.Open();

                    user.IdUser = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return user;



        }


        public int UpdateUser(User user)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "UPDATE [User] SET Name=@Name,Firstname=@Firstname,Adress=@Adress,Telephon=@Telephon,Email=@Email,Password=@Password,IdCity=@IdCity WHERE IdUser=@IdUser";
                    SqlCommand cmd = new SqlCommand(query, cn);


                    cmd.Parameters.AddWithValue("@IdUser", user.IdUser);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Adress", user.Adress);
                    cmd.Parameters.AddWithValue("@Telephon", user.Telephon);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Date", user.Date);
                    cmd.Parameters.AddWithValue("@IdCity", user.IdCity);


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
