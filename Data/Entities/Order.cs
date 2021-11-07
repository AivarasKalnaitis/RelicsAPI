using RelicsAPI.Auth.Model;
using RelicsAPI.Data.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.Entities
{
    public class Order : IUserOwnedResource
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedTimeUTC { get; set; }

        public ICollection<Relic> Relics { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
    public enum Status
    {
        Processing,
        Shipped,
        Received,
        Canceled
    }
}
