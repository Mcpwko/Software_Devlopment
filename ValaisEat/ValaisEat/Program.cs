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


        }
    }
}
