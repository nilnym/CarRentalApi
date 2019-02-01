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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        CustomersService customers;

        public CustomersController(CustomersService customers)
        {
            this.customers = customers;
        }

        [HttpGet]
        public async Task<CustomerItem[]> Get()
        {
            return await customers.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetails>> Get(int id)
        {
            try
            {
                return await customers.GetSingleAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
        }
    }
}