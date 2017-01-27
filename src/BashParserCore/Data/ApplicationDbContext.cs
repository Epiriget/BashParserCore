using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BashParserCore.Models;
using Microsoft.AspNetCore.Builder;


namespace BashParserCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      /*  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }*/

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
           .HasOne(p => p.Userpic)
           .WithOne(i => i.ApplicationUser)
           .HasForeignKey<Userpic>(b => b.ApplicationUserId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-BashParserCore-e9d3955e-f22b-44ff-8635-e4effd1641be;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Userpic> Userpics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
