using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Relics
{
    public record CreateRelicDTO(
        [Required] [MinLength(3)] string Name, 
        [Required] [MinLength(3)] string Creator, 
        [Required] decimal Price, 
        [Required] [MinLength(3)] string Period, 
        [Required] int YearMade
        );
}
