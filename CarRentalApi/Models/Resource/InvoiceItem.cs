using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Resource
{
    public class InvoiceItem
    {
        public string BookingId { get; set; }
        public decimal Cost { get; set; }
        public int TotalMileage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Car { get; set; }
        public string License { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerId { get; set; }
    }
}
