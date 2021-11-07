using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Auth
{
    public record RegisterUserDTO(
        [Required] string UserName, 
        [EmailAddress][Required] string Email, 
        [Required] string Password);
}
