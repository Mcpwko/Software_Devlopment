using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    //Allow only the customer to order and make a shopping cart
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


        //Show the shopping cart to the customer
        public ActionResult ShoppingCart()
        {
            var results = new List<Cart>();

            if (HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart") != null) {
            results = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            }
            var date = DateTime.Now;
            date.AddMinutes(15);

            var dt1 = RoundUp(date, TimeSpan.FromMinutes(15));
            var timeDay2 = dt1.TimeOfDay;


            ViewBag.Time = timeDay2;


            // Displays the shopping cart
            return View(results);
        }
        //Method that rounds the datetime to the nearest quarter of time
        public DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
        //Method that add an item into the shopping cart
        public ActionResult AddItem(int id)
        {

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
        //method that checks if a dish is already in the cart
        private int isExist(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].dish.IdDish.Equals(id))
                    return i;
            return -1;
        }
        //method that removes a dish from the cart
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

        //Remove the dish from the cart
        public ActionResult RemoveAllItems(int id)
        {
            List<Cart> cart = HttpContext.Session.GetObjectFromJson<List<Cart>>("Cart");

            var itemToRemove = cart.Single(r => r.dish.IdDish == id);
            cart.Remove(itemToRemove);

            HttpContext.Session.SetObjectAsJson("Cart", cart);


            return RedirectToAction("ShoppingCart");

        }

        //Method that creates the order
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

        //Show a confirmation of the order
        public ActionResult Confirmation()
        {
            return View();
        }
        //Show a message to explain that there is no courier available
        public ActionResult NoFreeCourier()
        {
            return View();
        }


    }
}