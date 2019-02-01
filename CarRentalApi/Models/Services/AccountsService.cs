using CarRentalApi.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Services
{
    public class AccountsService
    {
        UserManager<CustomIdentityUser> userManager;
        SignInManager<CustomIdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        IConfiguration configuration;

        public AccountsService(
            UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task CreateUserAsync(string username, string password)
        {
            var result = await userManager.CreateAsync(new CustomIdentityUser { UserName = username }, password);

            if (!result.Succeeded)
            {
                throw new ArgumentException(result.Errors.ToString());
            }
        }

        public async Task<string> CreateTokenAsync(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
                throw new KeyNotFoundException("User couldn't be authenticated.");

            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                throw new AuthenticationException("User couldn't be authenticated.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Token:Issuer"], configuration["Token:Issuer"], claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
