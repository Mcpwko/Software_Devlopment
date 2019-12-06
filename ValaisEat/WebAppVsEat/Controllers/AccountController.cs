using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace WebAppVsEat.Controllers
{
    
    public class AccountController : Controller
    {

        private IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [Authorize(Roles = "Customer")]
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Deliver")]
        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(DTO.User u)
        {
            DTO.User user = u;
            return View();
        }

        // GET: Account/Create
        public ActionResult Register()
        {
            return View();
        }


        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Login()
        {
            /**HttpContext.Session.SetString("Email",Username);*/
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DTO.User u)
        {
            try
            {
                UserManager um = new UserManager(Configuration);
                um.AddUser(u);
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
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