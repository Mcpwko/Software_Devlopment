using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        

        static void Main(string[] args)
        {
            var restaurantsManager = new RestaurantManager(Configuration);

            var restaurants = restaurantsManager.GetRestaurants();

            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant.ToString());
            }



            var cityManager = new CityManager(Configuration);

            var cities = cityManager.GetCities();

            foreach (var city in cities)
            {
                Console.WriteLine(city.ToString());
            }



            User user = new User();
            user.Name = "Follonier";
            user.Firstname = "Gregory";
            user.Adress = "Chemin des Collombes 211";
            user.Telephon = "023 322 25 65";
            user.Email = "gregory@hes.ch";
            user.Password = "password";
            user.IdCity = 1;
            user.Date = DateTime.Today;


            var userManager = new UserManager(Configuration);

            User user2 = userManager.AddUser(user);

            Console.WriteLine(user2.IdUser);







        }
    }
}
