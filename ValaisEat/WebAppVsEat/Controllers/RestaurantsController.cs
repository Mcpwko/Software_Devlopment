using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    public class RestaurantsController : Controller
    {
        // GET: Restaurants
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRestaurants()
        {
            var restaurantlist = new List<ListResto>
            {
                new ListResto(){ restaurants = new List<Restaurant> {
                    new Restaurant() {Name = "JoPizza", Adress = "Chemin des Collines 2" },
                    new Restaurant() {Name="Banana",Adress="Rue des amandiers"}
                    }
                }
            };
            return View(restaurantlist);
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int id)
        {
            var restaurants = new List<SelectListItem>
            {
                new SelectListItem {Value="1", Text="JOPIZZA"},
                new SelectListItem {Value="2", Text="BananRestaurant"},
                new SelectListItem {Value="3", Text="ChineseFood"}
            };
            ViewBag.Restaurants = restaurants;
            ViewBag.Selected = 3;

            return View();
        }

        public ActionResult Details(DTO.Restaurant r)
        {
            DTO.Restaurant restaurant = r;

            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
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
        }

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