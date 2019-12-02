using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class City
    {
        public int IdCity { get; set; }
        public int NPA { get; set; }
        public string CityName { get; set; }


        public override string ToString()
        {
            return $"{IdCity}|{NPA}|{CityName}";
        }
    }
}
