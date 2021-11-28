using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RelicsAPI.Data.DTOs
{
    public class UniqueCategoryName : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string strValue = value as string;

            using var context = new RelicsContext();

            if (!string.IsNullOrEmpty(strValue) && context.Categories.Any(c => c.Name == strValue))
                return false;

            return true;
        }
    }
}
