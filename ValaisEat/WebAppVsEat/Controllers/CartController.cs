using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{

    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {

        private IDishManager DishManager { get; set; }
        private ICourierManager CourierManager { get; set; }
        private IUserManager UserManager { get; set; }
        private ICustomerManager CustomerManager { get; set; }
        private IOrderManager OrderManager { get; set; }
        private IOrder_DishesManager Order_DishesManager { get; set; }
        private IRestaurantManager RestaurantManager { get; set; }

        public CartController(IDishManager dishManager, ICourierManager courierManager,IUserManager userManager, ICustomerManager customerManager, IOrderManager orderManager, IOrder_DishesManager order_DishesManager,IRestaurantManager restaurantManager)
        {
            DishManager = dishManager;
            CourierManager = courierManager;
            UserManager = userManager;
            CustomerManager = customerManager;
            OrderManager = orderManager;
            Order_DishesManager = order_DishesManager;
            RestaurantManager = restaurantManager;
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        // GET: Cart/Details/5
        public ActionResult ShoppingCart()
        {
            var results = new List<Cart>();

            if (HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") != null) {
            results = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            }


            var date = DateTime.Now;
            var timeDay = date.TimeOfDay;
            var nextFullHour = TimeSpan.FromHours(Math.Ceiling(timeDay.TotalHours));


            //ViewBag.Time = nextFullHour;
            date.AddMinutes(15);

            var dt1 = RoundUp(date, TimeSpan.FromMinutes(15));
            var timeDay2 = dt1.TimeOfDay;


            ViewBag.Time = timeDay2;


            // Displays the shopping cart
            return View(results);
        }

        public DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        public ActionResult AddItem(int id)
        {
            /**string id2 = id + "";
            var a = this.HttpContext.Session.GetString("Cart");

            
            HttpContext.Session.SetString("Cart", id2);


            Response.Cookies.Append("MyCookie", id2);*/

            if (HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { dish = DishManager.GetDish(id), quantity = 1, totalPriceProduct = DishManager.GetDish(id).Price });
                HttpContext.Session.SetObjectAsJson("Cart", cart);

            }
            else
            {
                int index = isExist(id);
                if (index != -1)
                {
                    List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
                    cart.Select(m => m.dish.IdDish == id);
                    cart[index].quantity++;
                    cart[index].totalPriceProduct += DishManager.GetDish(id).Price;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);

                }
                else
                {
                    List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
                    cart.Add(new Cart { dish = DishManager.GetDish(id), quantity = 1, totalPriceProduct = DishManager.GetDish(id).Price });
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }

            }


            return RedirectToAction(("ShoppingCart"));
        }

        private int isExist(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].dish.IdDish.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult RemoveItem(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            var itemToRemove = cart.Single(r => r.dish.IdDish == id);

            int index = isExist(id);
            if (cart[index].quantity >= 2)
            {
                cart[index].quantity--;
                cart[index].totalPriceProduct -= DishManager.GetDish(id).Price;
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }


            return RedirectToAction("ShoppingCart");
        }


        public ActionResult RemoveAllItems(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");

            var itemToRemove = cart.Single(r => r.dish.IdDish == id);
            cart.Remove(itemToRemove);

            HttpContext.Session.SetObjectAsJson("Cart", cart);


            return RedirectToAction("ShoppingCart");

        }


        public ActionResult Order(string deliverytime)
        {
            User user = UserManager.GetUserByEmail(User.Identity.Name);
            int idCustomer = CustomerManager.GetCustomerByIdUser(user.IdUser);
            var cartlists = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");

            int idresto = cartlists[0].dish.IdRestaurant;

            var idcity = RestaurantManager.GetCityFromRestaurant(idresto);

            var users = UserManager.GetUsersByIdCity(idcity);

            var couriers = CourierManager.GetCouriersByUserIdSameCity(users);

            //Check if there is a courier in the same city as the restaurant
            if (!couriers.Any())
            {
                return RedirectToAction("NoFreeCourier");
            }

            var orders = OrderManager.GetOrders();
            List<Courier> courierFree = new List<Courier>();

            //Retrieve the date of the order and the delivery date from the view ShoppingCart or Restaurants/Details
            string tspan = Convert.ToString(deliverytime);
            DateTime dt = DateTime.Now;
            DateTime ts = DateTime.Parse(tspan);


            foreach (var courier in couriers)
            {
                
                if (OrderManager.GetNumberOfOrder(courier.IdCourier,ts) < 5)
                {
                    courierFree.Add(courier);
                }
            }

            //Check if any courier is available
            if (!courierFree.Any())
            {
                return RedirectToAction("NoFreeCourier");
            }

            var courriers = CourierManager.GetCouriers();
            //@author : DeadEcho COEUR COEUR
            OrderManager.GetNumberOfOrder(courriers[0].IdCourier,ts);

            

            



            //Insert the order from the cart
            Order order = new Order();
            order.Status = "Not delivered";
            order.Date = dt;
            order.ShippingDate = ts;
            order.TotalPrice = cartlists.Sum(m => m.totalPriceProduct);
            order.IdCourier = courierFree[0].IdCourier;
            order.IdClient = idCustomer;

            Order order1 = OrderManager.AddOrder(order);


            //Insert each dish and quantity into Order_Dishes
            foreach(var product in cartlists)
            {
                Order_Dishes productordered = new Order_Dishes();
                productordered.Quantity = product.quantity;
                productordered.IdDish = product.dish.IdDish;
                productordered.IdOrder = order1.IdOrder;
                

                Order_DishesManager.AddOrder_Dishes(productordered);
            }

            var cart = new List<Cart>();

            HttpContext.Session.SetObjectAsJson("Cart", cart);


            return RedirectToAction("Confirmation","Cart");
            
        }


        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult NoFreeCourier()
        {
            return View();
        }


    }
}