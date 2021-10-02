using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Relics
{
    public record RelicDTO(int Id, string Name, string Creator, decimal Price, string Period, int YearMade);
}
