using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApi.Models.Resource;
using CarRentalApi.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        InvoicesService invoices;

        public InvoicesController(InvoicesService invoices)
        {
            this.invoices = invoices;
        }

        [HttpGet("{id}", Name = "GetInvoice")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var invoice = await invoices.GetInvoiceAsync(id);
                return Ok(invoice);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
