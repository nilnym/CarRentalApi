using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models
{
    public interface ICar
    {
        double BaseDayRental { get; set; }
        double NumberOfDays { get; set; }
        double KmPrice { get; set; }
        double NumberOfKm { get; set; }
    }
}
