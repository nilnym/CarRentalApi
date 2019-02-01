using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApi.Models.Identity
{
    public class CustomIdentityContext : IdentityDbContext<CustomIdentityUser>
    {
        public CustomIdentityContext(DbContextOptions<CustomIdentityContext> options) : base(options)
        {
            var result = Database.EnsureCreated();
        }
    }
}
