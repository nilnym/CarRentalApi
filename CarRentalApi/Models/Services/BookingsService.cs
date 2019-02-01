using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Binding;
using CarRentalApi.Models.Entities;
using CarRentalApi.Models.Resource;
using Microsoft.EntityFrameworkCore;

namespace CarRentalApi.Models.Services
{
    public class BookingsService
    {
        CarRentalContext context;

        public BookingsService(CarRentalContext context)
        {
            this.context = context;
        }

        private async Task<string> GenerateBookingNumberAsync()
        {
            Random random = new Random();

            string[] letters = new string[] {
                "A", "B", "C", "D", "E", "F", "G", "H", "I",
                "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X", "Y", "Z"
            };

            string code = "";

            for (int i = 0; i < 6; i++)
            {
                if (i < 2)
                    code += letters[random.Next(letters.Length)];
                else
                    code += random.Next(10).ToString();
            }

            if (await context.Booking.CountAsync(o => o.BookingId == code) > 0)
                return await GenerateBookingNumberAsync();
            else
                return code;
        }

        public async Task<string> CreateAsync(BookingStart model)
        {
            var bookingId = await GenerateBookingNumberAsync();

            var customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Ssn = model.SSN
            };

            await context.Customer.AddAsync(customer);

            var booking = new Booking
            {
                BookingId = bookingId,
                StartDate = model.StartDate,
                CustomerId = customer.Id,
                CarId = model.Car.Value,
                CarLicense = model.License,
                StartMileage = model.StartMileage.Value,
            };

            await context.Booking.AddAsync(booking);

            await context.SaveChangesAsync();

            return bookingId;
        }

        public async Task EndAsync(BookingEnd model)
        {
            var item = await context.Booking.SingleOrDefaultAsync(o => o.BookingId == model.BookingId);

            if (item == null)
                throw new KeyNotFoundException($"Couldn't find a booking matching {model.BookingId}.");

            if (item.EndMileage != null || item.EndDate != null)
                throw new InvalidOperationException("This booking is completed.");

            if (model.EndMileage < item.StartMileage)
                throw new ArgumentException("Final mileage can't be lower than starting mileage.");

            if (item.StartDate > model.EndDate)
                throw new ArgumentException("End date can't be before start date.");

            item.EndDate = model.EndDate;
            item.EndMileage = model.EndMileage;

            await context.SaveChangesAsync();
        }

        public async Task<BookingItem> GetSingleAsync(string id)
        {
            var booking = await context.Booking
                .Include(c => c.Car)
                .Include(p => p.Customer)
                .SingleOrDefaultAsync(o => o.BookingId == id);

            if (booking == null)
                throw new KeyNotFoundException($"Couldn't find a booking matching '{id}'.");

            return new BookingItem
            {
                BookingId = booking.BookingId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                StartMileage = booking.StartMileage,
                EndMileage = booking.EndMileage,
                Car = booking.Car.Name,
                License = booking.CarLicense,
                CustomerId = booking.CustomerId,
                FirstName = booking.Customer.FirstName,
                LastName = booking.Customer.LastName,
                isOpen = booking.EndDate == null ? true : false,
            };
        }

        public async Task<BookingItem[]> GetAllAsync()
        {
            return await context.Booking
                .Include(o => o.Car)
                .Include(o => o.Customer)
                .Select(o => new BookingItem
                {
                    BookingId = o.BookingId,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    StartMileage = o.StartMileage,
                    EndMileage = o.EndMileage,
                    Car = o.Car.Name,
                    License = o.CarLicense,
                    CustomerId = o.CustomerId,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    isOpen = o.EndDate == null ? true : false,
                })
                .OrderByDescending(o => o.isOpen)
                .ThenBy(o => o.BookingId)
                .ToArrayAsync();
        }
    }
}
