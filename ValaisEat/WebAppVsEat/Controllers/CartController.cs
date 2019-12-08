using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAppVsEat.Controllers
{
    

    public class CartController : Controller
    {

        private IDishManager DishManager { get; }

        public CartController(IDishManager dishManager)
        {
            DishManager = dishManager;
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        // GET: Cart/Details/5
        public ActionResult ShoppingCart()
        {
            string cookieValue = Request.Cookies["MyCookie"];

            string test2 = HttpContext.Session.GetString("Cart");
            
            List<Dish> results=null;

            if (results == null)
                results = new List<Dish>();

            foreach (var item2 in test2)
            {
            int i;
            Int32.TryParse(test2, out i);

            results.Add(DishManager.GetDish(i));
            }

            // Displays the shopping cart
            return View(results);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Cart/Create
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

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
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