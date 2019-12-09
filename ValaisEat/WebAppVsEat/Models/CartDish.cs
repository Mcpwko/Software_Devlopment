using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class CartDish
    {
        public List<Dish> ListA { get; set; }
        public List<Cart> ListB { get; set; }

        public double price { get; set; }

    }
}
