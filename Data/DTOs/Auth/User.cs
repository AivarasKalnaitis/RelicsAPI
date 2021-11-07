using Microsoft.AspNetCore.Identity;

namespace RelicsAPI.Data.DTOs.Auth
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
