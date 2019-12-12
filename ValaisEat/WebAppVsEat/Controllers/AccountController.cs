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
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IOrderManager OrderManager { get; set; }
        private IUserManager UserManager { get; set; }
        private ICourierManager CourierManager { get; set; }
        private ICustomerManager CustomerManager { get; set; }
        private IOrder_DishesManager Order_DishesManager { get; set; }
        private IDishManager DishManager { get; set; }
        private ICityManager CityManager { get; set; }

        public AccountController(IOrderManager orderManager, IUserManager userManager, ICourierManager courierManager,ICustomerManager customerManager,IOrder_DishesManager order_DishesManager, IDishManager dishManager, ICityManager cityManager)
        {
            OrderManager = orderManager;
            UserManager = userManager;
            CourierManager = courierManager;
            CustomerManager = customerManager;
            Order_DishesManager = order_DishesManager;
            DishManager = dishManager;
            CityManager = cityManager;
        }
        [Authorize(Roles = "Customer")]
        // GET: Account
        public ActionResult Customer()
        {
            string name = User.Identity.Name;
            User user = UserManager.GetUserByEmail(name);
            int idcustomer = CustomerManager.GetCustomerByIdUser(user.IdUser);

            var orderslist = new List<Order>();

            if (OrderManager.GetOrdersByCustomer(idcustomer) != null)
            {
                orderslist = OrderManager.GetOrdersByCustomer(idcustomer);
            }

            var descendingOrder = orderslist.OrderByDescending(i => i.IdOrder);
            var personalData = new PersonalData();
            var city = CityManager.GetCity(user.IdCity);

            personalData.orderlist = descendingOrder;
            personalData.user = user;
            personalData.city = city;

            return View(personalData);
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

            var orderCustomer = new List<OrderCustomer>();
            
            foreach (var item in descendingOrder)
            {
                var order = new OrderCustomer();
                order.order = item;
                var customer = CustomerManager.GetCustomer(item.IdClient);
                var customerdata = UserManager.GetUser(customer.IdUser);
                order.user = customerdata;
                order.city = CityManager.GetCity(customerdata.IdCity);

                orderCustomer.Add(order);
            }

            var deliverData = new DeliverData();
            deliverData.orderlist = orderCustomer;
            deliverData.user = user;
            deliverData.city = CityManager.GetCity(user.IdCity);

            return View(deliverData);
        
        }

        public ActionResult ConfirmDelivery(int id)
        {
            OrderManager.UpdateOrderStatus(id);

            return RedirectToAction("Deliver");
            
        }

        public ActionResult DetailsOrder(int id)
        {
            OrderManager.GetOrder(id);

            List<Cart> cartlist = new List<Cart>();

            var dishes = Order_DishesManager.GetOrder_Dishes(id);

            foreach(var item in dishes){
                Dish dish = DishManager.GetDish(item.IdDish);
                Cart cart = new Cart();
                cart.dish = dish;
                cart.quantity = item.Quantity;
                cart.totalPriceProduct = item.Quantity *dish.Price;
                cartlist.Add(cart);
            }

            return View(cartlist);

        }

        [Authorize(Roles ="Customer")]
        public ActionResult DeleteOrder(int id)
        {
            Order_DishesManager.DeleteOrder_Dishes(id);
            OrderManager.DeleteOrder(id);
            string name = User.Identity.Name;
            User user = UserManager.GetUserByEmail(name);
            int idcustomer = CustomerManager.GetCustomerByIdUser(user.IdUser);

            return RedirectToAction("Customer","Account");

        }


    }
}