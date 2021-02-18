using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProgram.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProgram.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SchoolClass> SchoolClass { get; set; }
        
    }
}
