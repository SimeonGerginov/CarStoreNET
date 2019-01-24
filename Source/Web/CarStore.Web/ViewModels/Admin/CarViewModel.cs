using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CarStore.Common;
using CarStore.Data.Models.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarStore.Web.ViewModels.Admin
{
    public class CarViewModel
    {
        public IEnumerable<SelectListItem> BrandsSelectList { get; set; }
        public IEnumerable<SelectListItem> ModelsSelectList { get; set; }
        public IEnumerable<SelectListItem> CategoriesSelectList { get; set; }
        public IEnumerable<SelectListItem> StoreCategoriesSelectList { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinCarName)]
        [MaxLength(GlobalConstants.MaxCarName)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [MinLength(GlobalConstants.MinDescriptionLength)]
        [MaxLength(GlobalConstants.MaxDescriptionLength)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Снимка")]
        public IFormFile Image { get; set; }
        
        [Display(Name = "Марка")]
        public string BrandName { get; set; }
        
        [Display(Name = "Модел")]
        public string ModelName { get; set; }

        [Required]
        [Range(GlobalConstants.MinYearLength, GlobalConstants.MaxYearLength)]
        [Display(Name = "Година на производство")]
        public int YearOfManufacture { get; set; }

        [Required]
        [Display(Name = "Цвят")]
        public Color Color { get; set; }

        [Required]
        [Display(Name = "Пробег")]
        public int Mileage { get; set; }

        [Required]
        [Display(Name = "Скоростна кутия")]
        public Gearbox Gearbox { get; set; }

        [Required]
        [Display(Name = "Тип на двигателя")]
        public EngineType EngineType { get; set; }
        
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }
        
        [Display(Name = "Категория в магазин")]
        public string StoreCategoryName { get; set; }
    }
}
