using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.DTOs.Orders
{
    public record OrderDTO(int Id, string Status, ICollection<Relic> Relics)
    {
    }
}
