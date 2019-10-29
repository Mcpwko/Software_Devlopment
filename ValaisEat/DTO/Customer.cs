using System;

namespace DTO
{
    public class Customer
    {
        public int IdClient { get; set; }
        public int IdUser { get; set; }


        public override string ToString()
        {
            return $"{IdClient}|{IdUser}";
        }
    }
}
