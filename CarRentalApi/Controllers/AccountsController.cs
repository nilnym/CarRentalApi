using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarRentalApi.Models.Binding;
using CarRentalApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarRentalApi.Controllers
{
    [Route("/accounts/[action]")]
    public class AccountsController : Controller
    {
        AccountsService service;

        public AccountsController(AccountsService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return BadRequest(new { error = "Missing username and/or password." });

            await service.CreateUserAsync(username, password);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { error = "Missing username and/or password." });

            try
            {
                var result = await service.CreateTokenAsync(model.Username, model.Password);

                return Ok(new { token = result });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
            catch (AuthenticationException e)
            {
                return Unauthorized(new { error = e.Message });
            }
        }
    }
}