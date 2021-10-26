using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RelicsAPI.Data.DTOs.Relics;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.DTOs.Orders
{
    public record CreateOrderDTO
    (
        [Required] string Status,
         ICollection<Relic> Relics
    );
}
