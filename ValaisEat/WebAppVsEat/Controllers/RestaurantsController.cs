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

namespace WebAppVsEat.Controllers
{
    [Authorize]
    public class RestaurantsController : Controller
    {
        private IConfiguration Configuration { get; }

        public RestaurantsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: Restaurants
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetRestaurants()
        {
            RestaurantManager rManager = new RestaurantManager(Configuration);
            var restaurantlist = rManager.GetRestaurants();
            return View(restaurantlist);
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id,string name)
        {
            DishManager rManager = new DishManager(Configuration);
            var dishes = rManager.GetDishes(id);


            ViewBag.nameResto = name;
            

            return View(dishes);
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