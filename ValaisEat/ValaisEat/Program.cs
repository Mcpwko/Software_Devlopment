using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

            var dishManager = new DishManager(Configuration);

            var dishes = dishManager.GetDishes(2);

            foreach (var dish in dishes)
            {
                Console.WriteLine(dish.ToString());
            }

            var userManager = new UserManager(Configuration);
            var animal = userManager.GetUser(1);

            Console.WriteLine(animal.ToString());



            var cityManager = new CityManager(Configuration);

            var cities = cityManager.GetCities();

            foreach (var city in cities)
            {
                Console.WriteLine(city.ToString());
            }


            var user2 = new User();

            user2.Name = "Kevin";
            user2.Firstname = "Coppey";
            user2.Adress = "Chemin des Etriettes 51";
            user2.Telephon = "025 423 23 11";
            user2.Email = "kevin@hes.ch";
            user2.Password = "Client2";
            user2.Date = DateTime.Today;
            user2.IdCity = 3;

            userManager.AddUser(user2);

            


        }
    }
}
