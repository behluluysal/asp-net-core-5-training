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
    }
}
