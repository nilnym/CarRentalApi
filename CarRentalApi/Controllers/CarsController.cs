using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Resource;
using CarRentalApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        CarsService cars;

        public CarsController(CarsService cars)
        {
            this.cars = cars;
        }

        [HttpGet]
        [Authorize]
        public async Task<CarListItem[]> Get()
        {
            return await cars.GetAllAsync();
        }
    }
}