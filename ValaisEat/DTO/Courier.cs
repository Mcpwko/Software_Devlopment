using System;

namespace DTO
{
    public class Courier
    {
        public int IdCourier { get; set; }
        public int IdUser { get; set; }


        public override string ToString()
        {
            return $"{IdCourier}|{IdUser}";
        }
    }
}
