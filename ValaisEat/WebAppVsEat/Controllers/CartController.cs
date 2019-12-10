using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppVsEat.Models;

namespace WebAppVsEat.Controllers
{
    

    public class CartController : Controller
    {

        private IDishManager DishManager { get; set; }
        private ICourierManager CourierManager { get; set; }
        private IUserManager UserManager { get; set; }
        private ICustomerManager CustomerManager { get; set; }
        private IOrderManager OrderManager { get; set; }
        private IOrder_DishesManager Order_DishesManager { get; set; }

        public CartController(IDishManager dishManager, ICourierManager courierManager,IUserManager userManager, ICustomerManager customerManager, IOrderManager orderManager, IOrder_DishesManager order_DishesManager)
        {
            DishManager = dishManager;
            CourierManager = courierManager;
            UserManager = userManager;
            CustomerManager = customerManager;
            OrderManager = orderManager;
            Order_DishesManager = order_DishesManager;
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

            var courrierlist = CourierManager.GetCouriers();

            string tspan = Convert.ToString(deliverytime);
            DateTime dt = DateTime.Now;
            DateTime ts = DateTime.Parse(tspan);



            //Insert the order from the cart
            Order order = new Order();
            order.Status = "Not delivered";
            order.Date = dt;
            order.ShippingDate = ts;
            order.TotalPrice = cartlists.Sum(m => m.totalPriceProduct);
            order.IdCourier = 1;
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


    }
}