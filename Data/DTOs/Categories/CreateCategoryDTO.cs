using System.ComponentModel.DataAnnotations;

namespace RelicsAPI.Data.DTOs.Categories
{
    public record CreateCategoryDTO([Required] [MinLength(3)] string Name);
}
