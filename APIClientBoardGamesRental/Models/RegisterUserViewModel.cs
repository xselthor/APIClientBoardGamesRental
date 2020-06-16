using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientBoardGamesRental.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Wpisane hasła nie pasują do siebie")]
        public string ConfirmPassword { get; set; }

        public string ShortName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string NIP { get; set; }
        public string ContactPerson { get; set; }
        public string Comments { get; set; }
        public string ContactPhone { get; set; }
    }
}
