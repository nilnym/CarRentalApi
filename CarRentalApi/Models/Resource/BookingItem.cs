using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Resource
{
    public class BookingItem
    {
        public string BookingId { get; set; }
        public string Car { get; set; }
        public string License { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StartMileage { get; set; }
        public int? EndMileage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CustomerId { get; set; }
        public bool isOpen { get; set; }
    }
}
