using System;
using System.Collections.Generic;

namespace CarRentalApi.Models.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
