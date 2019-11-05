using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Cities
    {
        public int IdCities { get; set; }
        public int NPA { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"{IdCities}|{NPA}|{City}";
        }
    }
}
