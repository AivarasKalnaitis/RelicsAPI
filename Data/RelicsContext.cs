using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data
{
    public class RelicsContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Relic> Relics { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=RelicsAPI");
        }

        
    }
}
