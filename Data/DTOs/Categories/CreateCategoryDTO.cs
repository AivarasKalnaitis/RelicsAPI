using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RelicsAPI.Data.DTOs.Categories
{
    public class CreateCategoryDTO
    {
        [Required] [MinLength(3)] [UniqueCategoryName(ErrorMessage = "Category name must be unique")]
        public string Name { get; set; }
    }
}