using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class OrderCustomer
    {
        public Order order { get; set; }
        public User user { get; set; }
        public City city { get; set; }

    }
}
