using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedTimeUTC { get; set; }

        public ICollection<Relic> Relics { get; set; }
    }

    public enum Status
    {
        Processing,
        Shipped,
        Received,
        Canceled
    }
}
