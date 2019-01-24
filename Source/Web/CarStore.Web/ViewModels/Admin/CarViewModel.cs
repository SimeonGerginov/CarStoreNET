using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;
using CarStore.Data.Models.Enums;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarStore.Web.ViewModels.Admin
{
    public class CarViewModel
    {
        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; }
        public IEnumerable<SelectListItem> StoreCategoriesSelectList { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCarName)]
        [MaxLength(GlobalConstants.MaxCarName)]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinBrandName)]
        [MaxLength(GlobalConstants.MaxBrandName)]
        public string BrandName { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinModelName)]
        [MaxLength(GlobalConstants.MaxModelName)]
        public string ModelName { get; set; }

        [Required]
        [Range(GlobalConstants.MinYearLength, GlobalConstants.MaxYearLength)]
        public int YearOfManufacture { get; set; }

        [Required]
        public Color Color { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public Gearbox Gearbox { get; set; }

        [Required]
        public EngineType EngineType { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCategoryName)]
        [MaxLength(GlobalConstants.MaxCategoryName)]
        public string CategoryName { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCategoryName)]
        [MaxLength(GlobalConstants.MaxCategoryName)]
        public string StoreCategoryName { get; set; }
    }
}
