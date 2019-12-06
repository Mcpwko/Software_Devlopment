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
        private IUserManager userManager2 { get; }
        private ICityManager cityManager2 { get; }
        public SignUpController(IUserManager userManager, ICityManager cityManager)
        {
            userManager2 = userManager;
            cityManager2 = cityManager;
        }

        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUp/SignUp
        public ActionResult SignUp()
        {

            var cities = cityManager2.GetCities();

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
        public ActionResult SignUp(User user)
        {
            try
            {
                
                userManager2.AddUser(user);
                Console.WriteLine("Success");



                return RedirectToAction("Index", "Home");
            }
            catch
            {
                Console.WriteLine("PIECE OF SHIT");
                return View();
            }
        }
    }
}