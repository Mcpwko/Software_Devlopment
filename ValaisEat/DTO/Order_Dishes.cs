using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Order_Dishes
    {
        public int Quantity { get; set; }
        public int IdDishes { get; set; }
        public int IdOrder { get; set; }


        public override string ToString()
        {
            return $"{IdOrder}|{IdDishes}|{Quantity}";
        }
    }
}
