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


            var dishManager = new DishManager(Configuration);

            List<Dish> dishes = new List<Dish>();

            var dish = dishManager.GetDish(1);

            var dish2 = dishManager.GetDish(2);

            dishes.Add(dish);
            dishes.Add(dish2);

            Console.WriteLine(dish.ToString());



            foreach (var plat in dishes)
            {
                Console.WriteLine(plat.ToString());
            }





        }
    }
}
