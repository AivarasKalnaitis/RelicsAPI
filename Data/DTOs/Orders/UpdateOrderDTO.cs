using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Orders
{
    public record UpdateOrderDTO([Required] string Status);
}
