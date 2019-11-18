using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Adress { get; set; }
        [Phone]
        public string Telephon { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string Password { get; set; }
        public DateTime Date { get; set; }
        public int IdCities { get; set; }


        public override string ToString()
        {
            return $"{IdUser}|{Name}|{Firstname}|{Adress}|{Telephon}|{Email}|{Password}|{Date}|{IdCities}";
        }
    }
}