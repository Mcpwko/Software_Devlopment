using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    public class LoginController : Controller
    {
        
        private IUserManager UserManager { get; }
        private ICustomerManager CustomerManager { get; }
        public LoginController(IUserManager userManager,ICustomerManager customerManager)
        {
            UserManager = userManager;
            CustomerManager = customerManager;
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login1)
        {
            string username = login1.username;
            string password = login1.password;
            User user = UserManager.GetUserByEmail(username);
            //Check the user name and password  
            //Here can be implemented checking logic from the database  
            
            
            if (UserManager.VerificateAuthentification(username,password))
            {

                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                string roles;

                if(CustomerManager.IsACustomer(user))
                { 
                    roles = "Customer";
                }else
                {
                    roles = "Deliver";
                }

                
                identity.AddClaim(new Claim(ClaimTypes.Role, roles));


                var principal = new ClaimsPrincipal(identity);

                



                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("password", "Please check your email or password and try again !");
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(HomeController.Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(HomeController.Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(HomeController.Index));
            }
            catch
            {
                return View();
            }
        }
    }
}