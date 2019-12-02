using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Salary
    {
        public int IdSalary { get; set; }
        public float Tips { get; set; }
        public int IdCourier { get; set; }


        public override string ToString()
        {
            return $"{IdSalary}|{Tips}|{IdCourier}";
        }
    }
}
