using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.Entities;
using System.Text.Json;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Relic>().Property(p => p.Materials)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, default),
                    v => JsonSerializer.Deserialize<List<string>>(v, default));

            builder.Entity<Order>().Property(r => r.Status)
                .HasConversion(v => v.ToString(),
                               v => (Status)Enum.Parse(typeof(Status),v ));

            //builder.Entity<Relic>(entity =>
            //{
            //    entity.Property(x => x.Image).HasColumnType("blob");
            //});
        }
    }
}
