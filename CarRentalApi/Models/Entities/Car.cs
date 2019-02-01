using System;
using System.Collections.Generic;

namespace CarRentalApi.Models.Entities
{
    public partial class Car
    {
        public Car()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CostFormula { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
