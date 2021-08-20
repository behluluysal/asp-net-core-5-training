using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rokWebsite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace rokWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Kingdom> Kingdoms { get; set; }
        public DbSet<BasePlayer> BasePlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Executives> Executives { get; set; }
        public DbSet<Members> Members { get; set; }
    }
}
