using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppVsEat.Models
{
    public class Restaurant
    {
        public int IdRestaurants { get; set; }
        public string Name { get; set; }
        public DateTime Openingdate { get; set; }
        public string Schedule { get; set; }
        public string Type { get; set; }
        public string Adress { get; set; }
        public int IdCities { get; set; }


        public override string ToString()
        {
            return $"{IdRestaurants}|{Name}|{Openingdate}|{Schedule}|{Type}|{Adress}|{IdCities}";
        }
    }
}
