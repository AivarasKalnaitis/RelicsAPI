using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelicsAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RelicsAPI.Data.DTOs.Auth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace RelicsAPI.Data
{
    public class RelicsContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Relic> Relics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=RelicsAPI");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Relic>().Property(p => p.Materials)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<string[]>(v));

            builder.Entity<Order>().Property(r => r.Status)
                .HasConversion(v => v.ToString(),
                               v => (Status)Enum.Parse(typeof(Status),v ));

            //builder.Entity<Relic>().Property(p => p.Image)
            //    .HasConversion(
            //        v => JsonConvert.SerializeObject(v),
            //        v => JsonConvert.DeserializeObject<byte[]>(v));

            //builder.Entity<Relic>(entity =>
            //{
            //    entity.Property(x => x.Image).HasColumnType("blob");
            //});
        }
    }
}
