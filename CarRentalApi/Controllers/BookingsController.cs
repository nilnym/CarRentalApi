using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Binding;
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
    public class BookingsController : ControllerBase
    {
        BookingsService bookings;

        public BookingsController(BookingsService bookings)
        {
            this.bookings = bookings;
        }

        [HttpGet]
        public async Task<BookingItem[]> Get()
        {
            return await bookings.GetAllAsync();
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<BookingItem>> Get(string id)
        {
            try
            {
                return await bookings.GetSingleAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookingStart booking)
        {
            var bookingId = await bookings.CreateAsync(booking);

            return CreatedAtAction(nameof(Get), new { id = bookingId }, new { id = bookingId });
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] BookingEnd booking)
        {
            try
            {
                await bookings.EndAsync(booking);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
