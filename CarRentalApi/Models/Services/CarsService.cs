using CarRentalApi.Models.Entities;
using CarRentalApi.Models.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Services
{
    public class CarsService
    {
        CarRentalContext context;

        public CarsService(CarRentalContext context)
        {
            this.context = context;
        }

        public async Task<CarListItem[]> GetAllAsync()
        {
            return await context.Car
                .Select(o => new CarListItem
                {
                    Id = o.Id,
                    Name = o.Name
                })
                .ToArrayAsync();
        }
    }
}
