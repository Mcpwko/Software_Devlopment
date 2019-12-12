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
        public int quantity { get; set; }
        public double totalPriceProduct { get; set; }

        public TimeSpan date { get; set; }
        

    }
}
