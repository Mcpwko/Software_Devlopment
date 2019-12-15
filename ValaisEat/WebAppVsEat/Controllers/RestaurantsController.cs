using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;
using BLL;
using Microsoft.AspNetCore.Authorization;
using DTO;


namespace WebAppVsEat.Controllers
{

    [Authorize]
    public class RestaurantsController : Controller
    {

        private IRestaurantManager RestaurantManager { get; }
        private IDishManager DishManager { get; }
        private ICityManager CityManager { get; }

        public RestaurantsController(IRestaurantManager restaurantManager,IDishManager dishManager,ICityManager cityManager)
        {
            RestaurantManager = restaurantManager;
            DishManager = dishManager;
            CityManager = cityManager;
        }
        //Show the list of all the restaurants
        public ActionResult Restaurants()
        {
            
            var restaurantlist = RestaurantManager.GetRestaurants();
            var citylist = CityManager.GetCities();

            var listrestaurant = from resto in restaurantlist
                                 join ville in citylist on resto.IdCity equals ville.IdCity
                                 select new Restaurants { restaurant=resto ,city=ville };

            return View(listrestaurant);
        }

        //Show all the dishes of one restaurant
        public ActionResult Details(int id)
        {
            
            var dishes = DishManager.GetDishes(id);


            ViewBag.nameResto = RestaurantManager.GetRestaurant(id).Name;
            var cartest = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            List<Cart> cartlist = cartest;
            var cart= new List<Cart>();

            if (HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") == null)
            {
                

                

            }
            else
            {
                if (!cartlist.Any(m => m.dish.IdRestaurant == id))
                {
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
                else
                {
                    cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
                }
                
            }

            var date = DateTime.Now;
            var timeDay = date.TimeOfDay;
            var nextFullHour = TimeSpan.FromHours(Math.Ceiling(timeDay.TotalHours));


            date.AddMinutes(15);
            
            var dt1 = RoundUp(date, TimeSpan.FromMinutes(15));
            var timeDay2 = dt1.TimeOfDay;


            ViewBag.Time = timeDay2;


            var viewModel = new CartDish();
            viewModel.ListA = dishes;
            viewModel.ListB = cart;
            double price = 0;

            foreach(var plat in viewModel.ListB)
            {
               price += plat.dish.Price;
            }

            viewModel.price = price;

            return View(viewModel);
            
        }
        //Round the datetime to the nearest quarter time
        public DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
        //Add an item to the shopping cart
        public ActionResult AddItem(int id)
        {
            
            if (HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { dish = DishManager.GetDish(id), quantity = 1,totalPriceProduct=DishManager.GetDish(id).Price});
                HttpContext.Session.SetObjectAsJson("Cart",cart);
                
            }
            else
            {
                int index = isExist(id);
                if(index!= -1)
                {
                    List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
                    cart.Select(m => m.dish.IdDish == id);
                    cart[index].quantity++;
                    cart[index].totalPriceProduct += DishManager.GetDish(id).Price;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);

                }
                else
                {
                    List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
                    cart.Add(new Cart { dish = DishManager.GetDish(id), quantity = 1, totalPriceProduct = DishManager.GetDish(id).Price });
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
                
            }

            Dish dish = DishManager.GetDish(id);

            int idResto = dish.IdRestaurant;
            string name = RestaurantManager.GetRestaurant(idResto).Name;

            return RedirectToAction("Details/" + idResto);
        }
        //Check if a dish already exists in the shopping cart
        private int isExist(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].dish.IdDish.Equals(id))
                    return i;
            return -1;
        }
        //Remove a dish from your cart
        public ActionResult RemoveItem(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            var itemToRemove = cart.Single(r => r.dish.IdDish == id);

            int index = isExist(id);
            if (cart[index].quantity >= 2)
            {
                cart[index].quantity--;
                cart[index].totalPriceProduct -= DishManager.GetDish(id).Price;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            Dish dish = DishManager.GetDish(id);

            int idResto = dish.IdRestaurant;
            
            return RedirectToAction("Details/"+ idResto);
        }

        //Remove the dish from your cart
        public ActionResult RemoveAllItems(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");

            var itemToRemove = cart.Single(r => r.dish.IdDish == id);
            cart.Remove(itemToRemove);

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            Dish dish = DishManager.GetDish(id);

            int idResto = dish.IdRestaurant;

            return RedirectToAction("Details/" + idResto);

        }
    }
}