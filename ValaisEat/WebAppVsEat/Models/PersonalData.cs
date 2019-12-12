using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class PersonalData
    {
        public User user { get; set; }
        public IOrderedEnumerable<Order> orderlist { get; set; }
        public City city { get; set; }

    }
}
