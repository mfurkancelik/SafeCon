
using System;
using System.Collections.Generic;

namespace SafeCon.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdCode { get; set; } // tck
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public string MobilePhone { get; set; }
        public DateTime LastLoginDate { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Request> Requests { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Offer> Offers { get; set; }
        public List<Payment> Payments { get; set; }

    }
}
