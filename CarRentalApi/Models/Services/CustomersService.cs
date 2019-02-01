using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Entities;
using CarRentalApi.Models.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Models.Services
{
    public class CustomersService
    {
        CarRentalContext context;

        public CustomersService(CarRentalContext context)
        {
            this.context = context;
        }

        public async Task<CustomerItem[]> GetAllAsync()
        {
            return await context.Customer
                .Select(o => new CustomerItem
                {
                    Id = o.Id,
                    FirstName = o.FirstName,
                    LastName = o.LastName
                })
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName)
                .ToArrayAsync();
        }

        public async Task<ActionResult<CustomerDetails>> GetSingleAsync(int id)
        {
            var customer = await context.Customer
                .Include(o => o.Booking)
                    .ThenInclude(a => a.Car)
                .SingleOrDefaultAsync(o => o.Id == id);

            if (customer == null)
                throw new KeyNotFoundException($"Couldn't find a customer with id {id}.");

            return new CustomerDetails
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                SSN = customer.Ssn,
                Bookings = customer.Booking.Select(o => new BookingItemId
                {
                    BookingId = o.BookingId,
                })
                .ToArray(),
            };
        }
    }
}
