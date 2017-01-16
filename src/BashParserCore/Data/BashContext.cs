using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BashParserCore.Models;

namespace BashParserCore.Data
{
    public class BashContext : DbContext
    {
        /*    public BashContext(DbContextOptions<BashContext> options) : base(options)
            {

            }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-BashParserCore-e9d3955e-f22b-44ff-8635-e4effd1641be;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
