using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace rokWebsite.Data
{
    public class KingdomDbContext : IdentityDbContext<User>
    {
        public KingdomDbContext(DbContextOptions<KingdomDbContext> options)
            : base(options)
        {

        }

        public DbSet<Kingdom> Kingdoms { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
    }
}
