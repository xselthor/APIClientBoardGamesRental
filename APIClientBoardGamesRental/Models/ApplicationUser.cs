using System;
using AspNetCore.Identity.MongoDbCore.Models;

namespace APIClientBoardGamesRental
{
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string ShortName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string NIP { get; set; }
        public string ContactPerson { get; set; }
        public string Comments { get; set; }
        public string ContactPhone { get; set; }

        public ApplicationUser() : base()
        {

        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
    }
}
