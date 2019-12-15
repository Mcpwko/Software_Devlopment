using System.Collections.Generic;
using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppVsEat.Controllers
{
    public class SignUpController : Controller
    {
        private IUserManager UserManager { get; }
        private ICityManager CityManager { get; }
        private ICustomerManager CustomerManager { get; }
        public SignUpController(IUserManager userManager, ICityManager cityManager, ICustomerManager customerManager)
        {
            UserManager = userManager;
            CityManager = cityManager;
            CustomerManager = customerManager;
        }

        //Sign up page
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

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            try
            {
                if (UserManager.UserAlreadyExist(user.Email)==false)
                {
                    var customer = new Customer();
                    
                    UserManager.AddUser(user);
                    customer.IdUser = user.IdUser;

                    CustomerManager.AddCustomer(customer);
                    return RedirectToAction("Index", "Home");

                }
                TempData["Message"] = "Email already registerd, try another email or log in !";
                return RedirectToAction(nameof(SignUpController.SignUp));

            }
            catch
            {
                TempData["Message"] = "Please fill every fields correctly!";
                return RedirectToAction(nameof(SignUpController.SignUp));
            }
        }
    }
}