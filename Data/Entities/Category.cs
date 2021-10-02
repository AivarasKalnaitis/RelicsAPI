using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreationDateUTC { get; set; }
    }
}
