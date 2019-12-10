using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using DTO;

namespace WebAppVsEat.Controllers
{
    
    public class AccountController : Controller
    {

        private IOrderManager OrderManager { get; set; }
        private IUserManager UserManager { get; set; }
        private ICourierManager CourierManager { get; set; }

        public AccountController(IOrderManager orderManager, IUserManager userManager, ICourierManager courierManager)
        {
            OrderManager = orderManager;
            UserManager = userManager;
            CourierManager = courierManager;
        }
        [Authorize(Roles = "Customer")]
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Deliver()
        {
            string name = User.Identity.Name;
            User user = UserManager.GetUserByEmail(name);
            Courier courier = CourierManager.GetCourierByUserId(user.IdUser);

            var orderslist = new List<Order>();

            if (OrderManager.GetOrdersByCourier(courier.IdCourier) != null)
            {
                orderslist = OrderManager.GetOrdersByCourier(courier.IdCourier);
            }

            var descendingOrder = orderslist.OrderByDescending(i => i.IdOrder);

            return View(descendingOrder);
        
        }

        public ActionResult ConfirmDelivery(int id)
        {
            OrderManager.UpdateOrderStatus(id);

            return RedirectToAction("Deliver");
            
        }


    }
}