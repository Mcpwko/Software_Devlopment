using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class DeliverData
    {
        public User user { get; set; }

        public City city { get; set; }
        public List<OrderCustomer> orderlist { get; set; }
        

    }
}
