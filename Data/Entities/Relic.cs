using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RelicsAPI.Data.Entities
{
    public class Relic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string[] Materials { get; set; }

        public string Creator { get; set; }

        public decimal Price { get; set; }

        public string Period { get; set; }

        public string YearMade { get; set; }

        public DateTime CreationTimeUTC { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
