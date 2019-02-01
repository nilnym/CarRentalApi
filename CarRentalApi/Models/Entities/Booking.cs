using System;
using System.Collections.Generic;

namespace CarRentalApi.Models.Entities
{
    public partial class Booking
    {
        public int Id { get; set; }
        public string BookingId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string CarLicense { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartMileage { get; set; }
        public int? EndMileage { get; set; }

        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
