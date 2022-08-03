using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FC_CRUD.Models;

namespace FC_CRUD.DB_Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Developers> developer { get; set; }
        public DbSet<Userinfo> Userinfos { get; set; }
    }
}
