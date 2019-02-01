using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Binding
{
    public class BookingStart
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "A chosen car is required.")]
        public int? Car { get; set; }

        [Required(ErrorMessage = "Starting mileage is required.")]
        public int? StartMileage { get; set; }

        [Required(ErrorMessage = "License number is required.")]
        public string License { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "SSN is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid SSN.")]
        public string SSN { get; set; }
    }
}
