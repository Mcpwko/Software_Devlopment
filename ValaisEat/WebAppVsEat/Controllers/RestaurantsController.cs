using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppVsEat.Models;
using BLL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DTO;
using System.Web;

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
        // GET: Restaurants
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Restaurants()
        {
            
            var restaurantlist = RestaurantManager.GetRestaurants();
            var citylist = CityManager.GetCities();

            var listrestaurant = from resto in restaurantlist
                                 join ville in citylist on resto.IdCity equals ville.IdCity
                                 select new Restaurants { restaurant=resto ,city=ville };

            return View(listrestaurant);
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id,string name)
        {
            
            var dishes = DishManager.GetDishes(id);

            
            ViewBag.nameResto = name;
            

            return View(dishes);
        }

        public ActionResult AddItem(int id)
        {
            string id2 = id + "";
            var a = this.HttpContext.Session.GetString("Cart");

            
            HttpContext.Session.SetString("Cart", id2);


            //Response.Cookies.Append("MyCookie", id2);

            

            return RedirectToAction("Restaurants");
        }


        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurants/Edit/5
        /**public ActionResult Edit(int id)
        {
            RestaurantManager rManager = new RestaurantManager(Configuration);
            var restaurant = rManager.GetRestaurant(id);
            return View(restaurant);
        }
        [HttpPost]
        public ActionResult Edit (DTO.Restaurant r)
        {
            RestaurantManager rManager = new RestaurantManager(Configuration);
            rManager.UpdateRestaurant(r);
            return RedirectToAction(nameof(GetRestaurants));

        }*/

        // POST: Restaurants/Edit/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}