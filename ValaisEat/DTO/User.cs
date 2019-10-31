using System;

namespace DTO
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Adress { get; set; }
        public string Telephon { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
        public int IdCities { get; set; }


        public override string ToString()
        {
            return $"{IdUser}|{Name}|{Firstname}|{Adress}|{Telephon}|{Email}|{Password}|{Date}|{IdCities}";
        }
    }
}