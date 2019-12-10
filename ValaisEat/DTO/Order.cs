using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Order
    {
        public int IdOrder { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public DateTime ShippingDate { get; set; }
        public double TotalPrice { get; set; }
        public int IdCourier { get; set; }
        public int IdClient { get; set; }


        public override string ToString()
        {
            return $"{IdOrder}|{Status}|{Date}|{ShippingDate}|{TotalPrice}|{IdCourier}|{IdClient}";
        }
    }
}
