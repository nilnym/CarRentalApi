using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Entities;
using CarRentalApi.Models.Resource;
using Jace;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Models.Services
{
    public class InvoicesService
    {
        CarRentalContext context;

        public InvoicesService(CarRentalContext context)
        {
            this.context = context;
        }

        public async Task<InvoiceItem> GetInvoiceAsync(string id)
        {
            var item = await context.Booking
                .Include(o => o.Car)
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(o => o.BookingId == id);

            if (item == null)
                throw new KeyNotFoundException($"Couldn't find a booking matching {id}.");

            if (item.EndDate == null || item.EndMileage == null)
                throw new InvalidOperationException("Booking is still in progress.");

            CalculationEngine engine = new CalculationEngine();
            Dictionary<string, double> vars = new Dictionary<string, double>();

            var formula = item.Car.CostFormula;

            var days = (item.EndDate.Value.Date - item.StartDate.Date).TotalDays;
            var mileage = item.EndMileage.Value - item.StartMileage;

            vars.Add(nameof(ICar.BaseDayRental), 200);
            vars.Add(nameof(ICar.NumberOfDays), Math.Max(1, days));
            vars.Add(nameof(ICar.NumberOfKm), mileage);
            vars.Add(nameof(ICar.KmPrice), 20);

            var result = engine.Calculate(formula, vars);

            return new InvoiceItem
            {
                BookingId = item.BookingId,
                Cost = (decimal)result,
                StartDate = item.StartDate,
                EndDate = item.EndDate.Value,
                TotalMileage = mileage,
                Car = item.Car.Name,
                License = item.CarLicense,
                CustomerId = item.CustomerId,
                FirstName = item.Customer.FirstName,
                LastName = item.Customer.LastName,
            };
        }
    }
}
