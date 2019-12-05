using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppVsEat.Controllers
{
    public class SignUpController : Controller
    {
        private IUserManager UserManager { get; }
        private ICityManager CityManager { get; }
        public SignUpController(IUserManager userManager, ICityManager cityManager)
        {
            UserManager = userManager;
            CityManager = cityManager;
        }

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignUp/SignUp
        public ActionResult SignUp()
        {

            var cities = CityManager.GetCities();

            var citieslist = new List<SelectListItem>();

            foreach (City c in cities)
            {
                citieslist.Add(new SelectListItem { Value = c.IdCity.ToString(), Text = c.CityName });
            }

            ViewBag.City = citieslist;
            return View();
        }

        // POST: SignUp/SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(DTO.User user)
        {
            try
            {
                
                UserManager.AddUser(user);



                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("SignUp","SignUp");
            }
        }

        // GET: SignUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignUp/Edit/5
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

        // GET: SignUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignUp/Delete/5
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