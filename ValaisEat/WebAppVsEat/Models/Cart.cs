using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class Cart
    {
        public Dish dish { get; set; }
        public Order_Dishes order_dish { get; set; }

    }
}
