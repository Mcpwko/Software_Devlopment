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



            var orderManager = new OrderManager(Configuration);

            var order = orderManager.GetOrder(14);

            double chiffre = (order.ShippingDate.TimeOfDay.TotalMinutes- DateTime.Now.TimeOfDay.TotalMinutes);

            Console.WriteLine(chiffre);






        }
    }
}
