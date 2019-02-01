using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Binding
{
    public class BookingEnd
    {
        [Required(ErrorMessage = "Booking Id is required.")]
        public string BookingId { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "End mileage is required.")]
        public int EndMileage { get; set; }
    }
}
