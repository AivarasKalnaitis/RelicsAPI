using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RelicsAPI.Data.Entities;

namespace RelicsAPI.Data.DTOs.Relics
{
    public record CreateRelicDTO(

        [Required] [MinLength(3)] string Name,

        List<string> Materials,

        string Creator,

        [Required] decimal Price,

        [Required] [MinLength(3)] string Period,

        [Required] byte[] Image,

        string YearMade
    );
}
