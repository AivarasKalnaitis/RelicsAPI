using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs.Categories
{
    public record UpdateCategoryDTO([Required] [MinLength(3)][UniqueCategoryName(ErrorMessage = "Category name must be unique")] string Name);
}
