using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Dishes
    {
        public int IdDishes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int IdRestaurants { get; set; }


        public override string ToString()
        {
            return $"{IdDishes}|{Name}|{Description}|{Price}|{Title}|{Status}|{IdRestaurants}";
        }
    }
}
