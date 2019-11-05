using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Salaries
    {
        public int IdSalaries { get; set; }
        public float Tips { get; set; }
        public int IdCourier { get; set; }


        public override string ToString()
        {
            return $"{IdSalaries}|{Tips}|{IdCourier}";
        }
    }
}
