using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LicentaFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LicentaFinal.Models.Instrument> Instrument { get; set; }
        public DbSet<LicentaFinal.Models.Cart> Cart { get; set; }
        public DbSet<LicentaFinal.Models.Sales> Sales { get; set; }
    }
}
