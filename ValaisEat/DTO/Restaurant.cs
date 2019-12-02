using System;

namespace DTO
{
    public class Restaurant
    {
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public DateTime Openingdate { get; set; }
        public string Schedule { get; set; }
        public string Type { get; set; }
        public string Adress { get; set; }
        public int IdCity { get; set; }
      

        public override string ToString()
        {
            return $"{IdRestaurant}|{Name}|{Openingdate}|{Schedule}|{Type}|{Adress}|{IdCity}";
        }
    }
}
