using System.Diagnostics;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    public class HomeController : Controller
    {
        //Homepage of the website
        public IActionResult Index()
        {
            return View();
        }
        
        //Logout the user
        [Authorize]
        public ActionResult DeleteCookie()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            Response.Cookies.Delete(User.Identity.Name);

            HttpContext.Session.Clear();

            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);


            return RedirectToAction("Index", "Home");

            
        }
        //How does it work page
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //Default error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
