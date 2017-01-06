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
        public BashContext(DbContextOptions<BashContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
