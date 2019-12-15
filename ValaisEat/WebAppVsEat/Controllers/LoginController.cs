using System.Security.Claims;
using BLL;
using DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        // Login page
        public ActionResult Login()
        {
            return View();
        }
        //Login the user when he clicks on the login button
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login1)
        {
            string username = login1.username;
            string password = login1.password;
            User user = UserManager.GetUserByEmail(username);
            //Check the user name and password  
            
            
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
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("password", "Please check your email or password and try again !");
            return View();
        }
    }
}