using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Relics
{
    public record UpdateRelicDTO(
        [Required] [MinLength(3)] string Name,

        List<string> Materials,

        [Required] byte[] Image,

        string Creator,

        [Required] decimal Price,

        [Required] [MinLength(3)] string Period,

        [RegularExpression("^([0-9]*$)|^((Unknown)*$)", ErrorMessage = "YearMade must be numeric")] string YearMade,

        [Required] int CategoryId
    );
}
